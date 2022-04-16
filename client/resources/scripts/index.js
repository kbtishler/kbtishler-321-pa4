const baseUrl = "https://localhost:7279/api/songs"
var songList =[];
var mySong = {};

function getSongs(){
    fetch(baseUrl).then(function (response){
                console.log(response);
                return response.json();           
            }).then(function (json){
                document.getElementById("cards").innerHTML = "";
        console.log("test")
        var songs = json;
        for(let i = 0; i < songs.length; i++) {
            console.log("for loop")
            const card = document.createElement('div');
            card.className = "card col-md-4 bg-dark text-white";
            card.innerHTML = `
                    <img src ="./resources/images/music.jpeg" class="card-img" alt="..."/>
                    <div class ="card-img-overlay">
                    <h5 class = "card-title">${songs[i].songTitle} Favorited: ${songs[i].favorited}</h5>
                    </div>
                    `
            document.getElementById("cards").appendChild(card);
        } 
            }).catch(function (error){
                    console.log(error);
                })
}

postSong = () => {
    let searchString = document.getElementById("title").value;
    const putSongApiUrl = baseUrl;
    fetch(putSongApiUrl, {
        method: "POST",
        headers: {
            "Accept": `application/json`,
            "content-Type": `application/json`,
        },
        body: JSON.stringify({
            songTitle: searchString,
            songTimestamp: new Date().toISOString(),
            deleted: "n",
            favorited: "n"
        })
    })
        .then((res) => res.text())
        .then((json) => {
            console.log(json)
            let html = ``;

            console.log(song.title)
            const card = document.createElement(`div`);
            card.className = "card col-md-4 bg-dark text-white";
            card.innerHTML =`
            <img src ="./client/resources/images/music.jpeg" class="card-img" alt="..."/>
            <div class = "card-img-overlay">
            <h5 class = "card-title">${searchString}</h5>
            </div>
            `
            document.getElementById("cards").appendChild(card);
        }).catch(function(error){
            console.log(error);
        }).finally(() => {
            getSongs();
        })
}

function deleteSong() {
    let searchString = document.getElementById("deleteid").value;
    console.log(searchString);
    const deleteSongUrl = baseUrl;
    console.log(deleteSongUrl);

    fetch(deleteSongUrl, {
        method: "DELETE",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify({
            songTitle: searchString,
            songTimeStamp: new Date().toISOString(),
            deleted: "Yes",
            favorited: "No" 
        })
    })
    .then((response) => {
        console.log(response);
        getSongs();
    });
}

function favoriteSongs() {
    let searchString = document.getElementById("favoriteid").value;
    console.log(searchString);
    const updateSongUrl = baseUrl;
    console.log(updateSongUrl);

    fetch(updateSongUrl,{
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify({
            songTitle: searchString,
            songTimeStamp: new Date().toISOString(),
            deleted: "n",
            favorited: "y"
        })
    })
    .then((response) => {
        console.log(response);
        getSongs();
    });
}