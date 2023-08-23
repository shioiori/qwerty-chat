import { createApp } from 'vue'
import App from './App.vue'
import './assets/css/app.css'

import store from './stores'
import router from './routers'

import mitt from 'mitt';

import hub from './hubs/chathub.js';

const emitter = mitt();

const app = createApp(App);

app.config.globalProperties.emitter = emitter;
app.use(router);
app.use(store);
app.use(hub);
app.mount('#app')