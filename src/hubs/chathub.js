import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import constants from '@/constants';

import mitt from 'mitt';

const emitter = mitt();

const connection = new HubConnectionBuilder()
                        .withUrl(constants.BASE_URL + '/chat-hub')
                        .configureLogging(LogLevel.Information)
                        .build();

const addToGroup = connection.on("AddToGroup", (message) => {
    console.log(message);
});

const receiveMessage = connection.on("ReceiveMessage", (message) => {
    console.log("received message")
    emitter.emit("sendMessage", JSON.parse(message));
    //hub.connection.off("ReceiveMessage");
});

async function onConnectionAsync() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
    }
}

function onDisconnectionAsync(){

}

function onConnectedNetwork(user_id){
    connection.invoke("OnConnectedNetwork", user_id).catch(function (err) {
        return console.error(err.toString());
    });
    return connection.on("OnConnected", (connection_id) => {
        return connection_id;
    });
}

async function SendAll(message){
    connection.invoke("SendAll", message).catch(function (err) {
        return console.error(err.toString());
    });
}

async function SendMessage(message){
    connection.invoke("SendGroup", message).catch(function (err) {
        return console.error(err.toString());
    });
}

async function AddToGroup(chat_id, user_id){
    connection.invoke("AddToGroup", chat_id, user_id).catch(function (err) {
        return console.error(err.toString());
    });
}

async function RemoveFromGroup(chat_id, user_id){
    connection.invoke("RemoveFromGroup", chat_id, user_id).catch(function (err) {
        return console.error(err.toString());
    });
}


export default {
    onConnectionAsync,
    onDisconnectionAsync,
    onConnectedNetwork,
    SendMessage,
    SendAll,
    AddToGroup,
    RemoveFromGroup,
    addToGroup,
    receiveMessage,
    emitter
}