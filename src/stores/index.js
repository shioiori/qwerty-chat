import axios from 'axios';
import { createStore } from 'vuex'
import constants from '../constants/index.js';


const store = createStore({
    state: () => ({ 
        user_id: '',
        access_token: '',
        user_connection: '',
        current_connection_user_id: '',
        current_connection_id: '', 
    }),
    getters: {
        getAccessToken: (state) => state.access_token,
        getUserId: (state) => state.user_id,
        getSearchConnection: (state) => async (name) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/user/search?search_value=' + name;
                var res = (await axios.get(url, config)).data;
                return res;
            }
            catch (err) {
                console.log(err);    
            }
        },
        getUser: (state) => async (id) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/user' + (id ? '?id=' + id : '');
                var res = (await axios.get(url, config)).data;
                return res;
            }
            catch (err) {
                console.log(err);    
            }
        },
        getCurrentChat: (state) => async (id) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/chat/get-current-chat?id=' + id;
                var res = (await axios.get(url, config)).data;
                return res;
            }
            catch (err) {
                console.log(err);    
            }
        },
        getListMessages: (state) => async (chat_id) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/message/get-messages?chat_id=' + chat_id;
                var res = (await axios.get(url, config)).data;
                return res;
            }
            catch (err) {
                console.log(err);    
            }
        },
        getCurrentConnection: async (state)  => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/chat/get-list-connection';
                var res = (await axios.get(url, config)).data;
                return res;
            }
            catch (e) {
                console.log(e.message)
            }
        },
        createCurrentChat: (state) => async (member_ids, chat_name, is_limited) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/chat/create-new-chat?name=' + chat_name + '&is_limited=' + is_limited;
                let body = member_ids;
                var res = (await axios.post(url, body, config)).data;
                return res;
            }
            catch (err) {
                console.log(err);    
            }
        },
        checkUserInChat: (state) => async (member_ids, is_limited) => {
            try {
                let config = {
                    headers: {
                        Authorization: "Bearer " + state.access_token
                    }
                }
                let url = constants.BASE_URL + '/api/chat/check-user-in-chat?is_limited=' + is_limited;
                let body = member_ids;
                console.log(body)
                var res = (await axios.post(url, body, config)).data;
                return res;
            }
            catch (e) {
                console.log(e.message)
            }
        },
    },
    mutations: {
        setAccessToken: (state, token) => state.access_token = token,
        setUserId: (state, userId) => state.user_id = userId,
        setCurrentConnectionUserId: (state, connection) => state.current_connection_user_id = connection,
        setCurrentConectionId: (state, connection) => state.current_connection_id = connection,
    },
    actions: {
        
    }
});

export default store;
  