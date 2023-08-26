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

export default {
    onConnectionAsync,
    onDisconnectionAsync,
    onConnectedNetwork,
    SendMessage,
    SendAll,
    connection,
}