<template>
    <div class="grid grid-cols-12 h-screen">
        <ChatSideBar/>
        <ChatListContact/>
        <ChatContent/>
    </div>
</template>

<script>

import ChatSideBar from "../components/ChatSideBar.vue";
import ChatListContact from "../components/ChatListContact.vue";
import ChatContent from "../components/ChatContent.vue";
import router from '../routers';
import hub from "@/hubs/chathub";

export default {
    name: "TheChat",
    components: {
        ChatSideBar,
        ChatListContact,
        ChatContent
    },
    beforeCreate(){
        if (this.$store.getters.getAccessToken == ""){
            router.push('/login');
        }
    },
    created(){
        hub.onConnectionAsync();
        hub.onConnectedNetwork(this.$store.getters.getUserId);
    },
    methods: {

    }
}
</script>

<style>

</style>