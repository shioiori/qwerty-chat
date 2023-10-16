<template>
    <input type="text" placeholder="Type here" class="input input-bordered w-full" v-model="search_value" v-on:input="search"/>
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
            setTimeout(() => {
                this.$emit('search-connection', this.search_value);   
            }, 350);
        }
    }
}
</script>

<style>

</style>