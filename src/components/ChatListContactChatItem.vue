<template>
    <div class="one-user flex my-4">
        <div class="avatar online">
            <div class="w-12 rounded">
                <img :src="chat.avatar" />
            </div>
        </div>
        <div class="mx-3">
            <p>{{ this.username }}</p>
            <span class="badge badge-accent">Active</span>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        chat: Object,
    },
    data(){
        return {
            id: '',
            username: '',
            avatar: '',
        }
    },
    beforeMount(){
        this.bindData();
    },  
    mounted(){
        this.bindData();
    },
    methods: {
        async bindData(){
            try{
                this.id = this.chat.id;
                this.id = this.$store.getters.getUserId;
                this.username = this.chat.name ? this.chat.name : (this.chat.members[0].id == this.id ? this.chat.members[1].name : this.chat.members[0].name);
            }
            catch (e) {
                console.log(e.message);
            }
        }
    }
}
</script>

<style>

</style>