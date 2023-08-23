<template>
    <div class="one-user flex my-4">
        <div class="avatar online">
            <div class="w-12 rounded">
                <img :src="user.avatar" />
            </div>
        </div>
        <div class="mx-3">
            <p>{{ this.user.name }}</p>
            <span class="badge badge-accent">Active</span>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        user: Object,
    },
    data(){
        return {
        }
    },
    beforeMount(){
    },  
    mounted(){
        console.log(this.user)
    },
    methods: {
    }
}
</script>

<style>

</style>