<template>
    
    <div class="chat-message">
         <div class="flex items-end justify-end p-2">
            <div class="flex flex-col space-y-2 max-w-xs mx-2 order-1 items-end">
                <ChatContentBodyMessageItem :message="this.message"/>
            </div>
            <img :src="this.user.avatar" class="w-6 h-6 rounded-full order-2">
         </div>
      </div>
</template>

<script>
import ChatContentBodyMessageItem from './ChatContentBodyMessageItem.vue';
export default {
    data() {
        return {
            user: {},
        }
    },
    props: {
        message: Object
    },
    components: {
        ChatContentBodyMessageItem,
    },
    beforeMount(){
        this.getUser();
    },
    methods: {
        async getUser(){
            this.user = this.$store.getters.getUser(this.message.id);
        }
    }
}
</script>

<style>

</style>