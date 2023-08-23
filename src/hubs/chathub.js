import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import constants from '@/constants';

const connection = new HubConnectionBuilder()
                        .withUrl(constants.BASE_URL + '/chat-hub')
                        .configureLogging(LogLevel.Information)
                        .build();

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

async function SendMessage(message){
    console.log(message);
    connection.invoke("SendGroup", message).catch(function (err) {
        return console.error(err.toString());
    });
    return connection.on("ReceiveMessage", (obj) => {
        return JSON.parse(obj);
    });
}

export default {
    onConnectionAsync,
    onDisconnectionAsync,
    onConnectedNetwork,
    SendMessage,
}