<template>
    <div class="chat-container">
        <ChatContentBodyMessage v-for="(item, index) in list_messages" v-bind:message="item" v-bind:key="index" />
    </div>
</template>

<script>
import ChatContentBodyMessage from './ChatContentBodyMessage.vue';

export default {
    data() {
        return {
            list_messages: [],
        }
    },
    props: {
        chat_info: Object
    },
    components: {
        ChatContentBodyMessage,
    },
    methods: {
        async bindMessage() {
            console.log(this.chat_info)
            this.list_messages = await this.$store.getters.getListMessages(this.chat_info.chat_id);
        }
    },
    mounted() {
        this.bindMessage();
    }
}
</script>

<style></style>