import Vue from 'vue';
import Vuex from 'vuex';
import VueSessionStorage from "vue-sessionstorage";
Vue.use(VueSessionStorage);
Vue.use(Vuex);

export const store = new Vuex.Store({
    strict: true,
    state: {
        userInfo: {
            ipdmsUser: -1,
            firstname: null,
            lastname: "",
            middlename: "",
            userRoleId: -1,
            email: "",
            password: ""
        },

    },
    mutations: {
        setUserInfo: (state, payload) => {
            state.userInfo = payload;
            //this.$session.set("userInfo", payload);
            sessionStorage.setItem("userInfo", JSON.stringify(payload));
            //console.log(payload);
            //console.log(JSON.parse(sessionStorage.getItem('userInfo')));
        }
    },
    actions: {
        setUserInfo: (context, payload) => {
            context.commit('setUserInfo', payload)
        }
    }
    //getters: {
    //    getUserInfo: (user) => {

    //    }
    //}
    //,
    //getters: {
    //    saleProducts: (state) => {
    //        var saleProducts = state.products.map(product => {
    //            return {
    //                name: '**' + product.name + '**',
    //                price: product.price / 2,
    //            };
    //        });
    //        return saleProducts;
    //    }
    //},
    //mutations: {
    //    reducePrice: (state, payload) => {
    //        state.products.forEach(product => {
    //            product.price -= payload
    //        });
    //    }
    //},
    //actions: {
    //    reducePrice: (context, payload) => {
    //        setTimeout(function () { // reach out for data
    //            context.commit('reducePrice', payload);
    //        }, 2000);
    //    }
    //}
});
