<template>
    <div id="list-contact" class="col-span-3 border-2 border-gray-100">
        <div class="user-contact-info border-b-2 border-gray-100 flex items-center p-4">
            <p class="text-xl flex-auto font-bold">Chat</p>
            <ChatListContactGroupButton/>
        </div>
        <div class="lst-contact my-4 px-4">
            <ChatListContactSearch
                @search-connection="searchConnection"/>
            <ChatListContactChatItem 
                v-for="(item, index) in chat_connection"
                v-bind:chat="item"
                v-bind:key="index"
                @click="getCurrentChatById(item.id)" 
            />
            <ChatListContactUserItem 
                v-for="(item, index) in user_connection"
                v-bind:user="item"
                v-bind:key="index"
                @click="getCurrentChatByUser(item.id, true)" 
            />
        </div>
    </div>
</template>

<script>
import ChatListContactGroupButton from './ChatListContactGroupButton.vue';
import ChatListContactChatItem from './ChatListContactChatItem.vue';
import ChatListContactSearch from './ChatListContactSearch.vue';
import ChatListContactUserItem from './ChatListContactUserItem.vue';

export default {
    components: {
        ChatListContactGroupButton,
        ChatListContactChatItem,
        ChatListContactUserItem,
        ChatListContactSearch,
    },
    data(){
        return {
            chat_connection: [],
            user_connection: [],
        }
    },
    beforeMount(){
        this.getCurrentChatConnection();
    },
    methods: {
        async searchConnection(search_value){
            if (search_value.trim() == ""){
                this.user_connection = [];
                this.getCurrentChatConnection();
                return;
            }
            this.user_connection = await this.$store.getters.getSearchConnection(search_value)
            this.chat_connection = [];
        },
        async getCurrentChatConnection(){
            this.chat_connection = await this.$store.getters.getCurrentConnection;
        },
        async getCurrentChatById(chat_id){
            var chat = await this.$store.getters.getCurrentChat(chat_id);
            this.emitter.emit("getCurrentChat", {
                chat_id: chat.id,
                on_chatted: true,
                chat_name: this.getChatName(chat),
                member_ids: chat.memberIds
            });
        },
        async getCurrentChatByUser(user_id, is_limited){
            var chat = await this.$store.getters.checkUserInChat([this.$store.getters.getUserId, user_id], is_limited);
            console.log(chat);
            if (!chat){
                chat = await this.$store.getters.createCurrentChat([this.$store.getters.getUserId, user_id], "", is_limited)
            }
            this.emitter.emit("getCurrentChat", {
                chat_id: chat.id,
                on_chatted: true,
                chat_name: this.getChatName(chat),
                member_ids: chat.memberIds
            });
        },
        getChatName(chat){
            let id = this.$store.getters.getUserId;
            console.log(chat.members[0].id, id,chat.members[1].name);
            let username = chat.name ? chat.name : (chat.members[0].id == id ? chat.members[1].name : chat.members[0].name);
            return username;
        },
    }
}
</script>

<style>

</style>