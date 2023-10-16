<template>
    <div id="main-chat" class="col-span-8 border-r-2 border-gray-100" v-if="on_chatted">
        <ChatContentHeader :chat_info="chat_info"/>
        <ChatContentBody :chat_info="chat_info" :list_messages="list_messages"/>
        <ChatContentMessageBar :chat_info="chat_info"/>
    </div>
</template>

<script>
import ChatContentHeader from './ChatContentHeader.vue';
import ChatContentBody from './ChatContentBody.vue';
import ChatContentMessageBar from './ChatContentMessageBar.vue';
import hub from '../hubs/chathub.js';

export default {
    data(){
        return {
            on_chatted: false,
            chat_info: {},
            list_messages: [],
        }
    },
    components: {
        ChatContentBody,
        ChatContentHeader,
        ChatContentMessageBar,
    },
    mounted(){
        this.emitter.on("getCurrentChat", res => {
            this.on_chatted = res.on_chatted;
            this.chat_info = res;
            this.bindMessage(this.chat_info.chat_id);
            hub.AddToGroup(this.chat_info.chat_id, this.$store.getters.getUserId);
        });
        hub.emitter.on("sendMessage", message => {
            this.list_messages.push(message);
        });
    },
    methods: {
        async bindMessage(id) {
            this.list_messages = await this.$store.getters.getListMessages(id);
        },
    }
}
</script>

<style>
#main-chat{
    height: 100vh;
}
</style>