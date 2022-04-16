using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Collections.Generic;
using mis321_pa4_kbtishler.Models;
using mis321_pa4_kbtishler.api.Models.Interfaces;
using repos.mis321_pa4_kbtishler.api.Database;

namespace api.Utilities
{
    public class SongUtilDatabase : ISongUtilities
    {
        public List<Song> playlist { get; set; }
         public void PrintPlaylist() { // display all items in the playlist to the console
            Console.Clear();
            playlist.Sort();
            foreach (Song song in playlist) { // for every song in the playlist, write the song's ToString to the console
                if(song.Deleted != "y"){
                    Console.WriteLine(song.ToString());
                }
            }
            Console.WriteLine();
        }

        public void AddSong() { // allow the user to add a new song to the playlist
            int newID;

            if (playlist.Count != 0) { // sort the playlist so that the newest song added has the highest SongID
                playlist.Sort();
                newID = playlist[0].SongID + 1;
            }
            else {
                newID = 1;
            }

            playlist.Add(new Song(){SongID = newID, SongTitle = PromptSongDetails(), SongTimestamp = DateTime.Now, Deleted = "n"});    
            
            WriteToDatabase.WriteAllToDatabase(playlist);
            
        }

        public string PromptSongDetails() { // Ask user for title of the song to add
            Console.Clear();
            Console.WriteLine("What is the title of your song?");
            return Console.ReadLine();
        }

        public void DeleteSong() { // remove a song from the playlist given the SongID
            int index;
            
            do
            {
                // find index
                int IDToDelete = PromptSongToDelete(playlist);
            
                index = playlist.FindIndex(currentSong => currentSong.SongID == IDToDelete); // iterate through songs in playlist and compare their IDs to the ID the user wants to delete 

            } while (!CheckValidIndex(index)); // make sure the song ID exists - if playlist.FindIndex returns -1, the ID was not found in the list


            // remove song at index found
            string titleToDelete = playlist[index].SongTitle;
            playlist.RemoveAt(index);
            //updates the database
            WriteToDatabase.WriteAllToDatabase(playlist);
            Console.Clear();
            Console.WriteLine("{0} has been removed.", titleToDelete);
            
        }

        public int PromptSongToDelete(List<Song> playlist) { // ask the user the ID of the song they want to delete
            
            string userInput;
            
            do {

                Console.Clear();
                PrintPlaylist();
                Console.WriteLine("What is the ID of the song you want to delete?");
                userInput = Console.ReadLine();

            } while (!CheckValidInput(userInput)); // ID entered must be an integer
            
            return int.Parse(userInput);
        }

        public bool CheckValidIndex(int index) {
            if (index == -1) { // if playlist.FindAt returns -1, the ID was not found in the list
                Console.WriteLine("\nID does not exist in the current playlist. Press any key to continue");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        public bool CheckValidInput(string userInput) { // check to see if user's input is an integer
            int parsedInput;

            if (!int.TryParse(userInput, out parsedInput)) {
                Console.WriteLine("Invalid input. Try again.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        public void EditSong()
        {
            string userChoice;
            int index;
            int choiceParsed;

            do{
                
                do{
                    Console.Clear();
                    PrintPlaylist();
                    Console.WriteLine("Enter the ID of the song you wish to edit:");
                    userChoice = Console.ReadLine();
                    choiceParsed = int.Parse(userChoice);
                } while(!CheckValidInput(userChoice));//checks to made sure that the given Id is valid

                index = playlist.FindIndex(currentSong => currentSong.SongID == choiceParsed);//finds the ID in the playlist and then deletes it
            }while(!CheckValidIndex(index));

            Console.WriteLine("Enter the new title of the song:");
            string title = Console.ReadLine();

            //title of the edited song is now updated
            playlist[index].SongTitle = title;
            //the updated list is written to the database
            WriteToDatabase.WriteAllToDatabase(playlist);

        }
    }
}