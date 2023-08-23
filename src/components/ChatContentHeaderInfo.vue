<template>
    <div class="relative flex items-center space-x-4">
        <div class="relative">
            <span class="absolute text-green-500 right-0 bottom-0">
               <svg width="20" height="20">
                  <circle cx="8" cy="8" r="8" fill="currentColor"></circle>
               </svg>
            </span>
         <img :src="this.avatar" alt="" class="w-10 sm:w-16 h-10 sm:h-16 rounded-full">
         </div>
         <div class="flex flex-col leading-tight">
            <div class="text-2xl mt-1 flex items-center">
               <span class="text-gray-700 mr-3">{{ this.chat_info.chat_name }}</span>
            </div>
         </div>
    </div>
</template>

<script>
export default {
    data(){
        return {
            user_id: '',
            name: '',
            avatar: '',
        }
    },
    props: {
        chat_info: Object
    },
    mounted(){
    },
    methods: {
    },
}
</script>

<style>

</style>