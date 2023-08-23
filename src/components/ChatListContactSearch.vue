<template>
    <input type="text" placeholder="Type here" class="input input-bordered w-full" v-model="search_value" @keyup.enter="search"/>
</template>

<script>
export default {
    data(){
        return {
            search_value: '',
        }
    },
    methods: {
        search(){
            this.$emit('search-connection', this.search_value);   
        }
    }
}
</script>

<style>

</style>