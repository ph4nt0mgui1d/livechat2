"use strict"

//Get User List
$(document).ready(function () {
    $.ajax({
        url: '/Home/GetUser',
        type: 'GET',
        datatype: 'Json',
        success: function (userlist) {
            console.log(userlist)
            //alert(data)
            $.each(userlist, function (index, value) {
               // console.log(value.userName)
                $('#usercnt').append(`<div class="friend-drawer friend-drawer--onhover" id="chatid">
                <img class="profile-image" src="https://i.pinimg.com/736x/8b/16/7a/8b167af653c2399dd93b952a48740620.jpg" alt="">
                <div class="text">
                    <h6>${value.userName}</h6>
                    <p class="text-muted">${value.id}</p>
                </div>
                <span class="time text-muted small">13:21</span>
            </div>`);
            })
        }
    });
});

//Select Conversation
$(document).on('click', '#chatid', function () {
    let id = $(this).children('div').children('p').html();
    let nameUser = $(this).children('div').children('h6').html();
    //alert(id)
    createScreen(id, nameUser);
});

//Create Chat Screen
function createScreen(id, uname) {
    $('#chatUserName').html(uname);
    $('#chatUserId').html(id);

}


//send msg
function sendmsg(e) {
    //alert(keycode);
    var key = event.key;
    
    if (key == "Enter") {

        var msg = document.getElementById("msg");
        var receiver = $("#chatUserId").text();
        //alert(receiver);
        // call ajex
        let data = { text: msg.value, receiver: receiver };
        $.ajax({
            url: '/Home/AddMsg',
            type: 'POST',
            data: data,
            success: function (data) {
                msg.value = "";
                msg.focus();
                //alert(data)
            }
        });
    }
}


// get live chat using signalR

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
connection.start();
connection.on("ReceiveMsg", function (msg) {
    //alert("Submitted");
    const para = document.createElement("p");
    const node = document.createTextNode(msg);
    para.classList.add("chat-bubble", "offset-md-9");
    para.appendChild(node);

    const element = document.getElementById("msgList");
    element.appendChild(para);
})