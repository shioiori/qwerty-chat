<template>
    <div class="chat-message py-1">
         <div class="flex items-end" :class="this.message.user.id == this.$store.getters.getUserId ? 'justify-start' : 'justify-end'">
            <div class="flex flex-col space-y-2 text-xs max-w-xs mx-2 order-2">
                <ChatContentBodyMessageItem :message="this.message"/>
            </div>
         </div>
      </div>
</template>

<script>
import ChatContentBodyMessageItem from './ChatContentBodyMessageItem.vue';
export default {
    data() {
        return {
            position: 'item-start',
        }
    },
    props: {
        message: Object
    },
    components: {
        ChatContentBodyMessageItem,
    },
    computed: {
    }
}
</script>

<style>

</style>