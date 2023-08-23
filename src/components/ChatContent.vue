<template>
    <div id="main-chat" class="col-span-5 border-r-2 border-gray-100" v-if="on_chatted">
        <ChatContentHeader :chat_info="chat_info"/>
        <ChatContentBody :chat_info="chat_info"/>
        <ChatContentMessageBar :chat_info="chat_info"/>
    </div>
</template>

<script>
import ChatContentHeader from './ChatContentHeader.vue';
import ChatContentBody from './ChatContentBody.vue';
import ChatContentMessageBar from './ChatContentMessageBar.vue';

export default {
    data(){
        return {
            on_chatted: false,
            chat_info: {}
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
            console.log(this.chat_info);
        });
    }
}
</script>

<style>

</style>