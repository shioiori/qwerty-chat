<template>
    <div class="chat-container">
        <div>
            <ChatContentBodyMessage v-for="(item, index) in list_messages" v-bind:message="item" v-bind:key="index" />
        </div>
    </div>
</template>

<script>
import ChatContentBodyMessage from './ChatContentBodyMessage.vue';

export default {
    data() {
        return {
            //current_list_messages: this.list_messages,
        }
    },
    props: {
        chat_info: Object,
        list_messages: Array,
    },
    components: {
        ChatContentBodyMessage,
    },
    mounted(){
        // this.emitter.on("sendMessage", message => {
        //     console.log(this.current_list_messages)
        //     console.log(message);
        //     this.current_list_messages.push(message);
        // });
    },
    methods: {
    }
}
</script>

<style>
.chat-container{
    height: 77%;
    overflow: auto;
    display: flex;
    flex-direction: column-reverse;
}
</style>