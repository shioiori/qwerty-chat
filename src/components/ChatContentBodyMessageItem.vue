<template>
    <div>
        <span class="px-4 py-2 rounded-lg inline-block bg-blue-600 text-white" :class="this.message.user.id == this.$store.getters.getUserId ? 'rounded-bl-none' : 'rounded-br-none'">
            {{message.text}}
        </span>
    </div>
</template>
<script>
export default {
    props: {
        message: Object
    }
}
</script>

<style>

</style>