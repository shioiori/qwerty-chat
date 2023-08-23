<template>
    <div>
        <span class="px-4 py-2 rounded-lg inline-block rounded-br-none bg-blue-600 text-white ">
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