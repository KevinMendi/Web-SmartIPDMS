import Vue from 'vue'
import router from './router'

import BootstrapVue from "bootstrap-vue"
import App from './App'
import VueSessionStorage from "vue-sessionstorage";

import Default from './Layout/Wrappers/baseLayout.vue';
import Pages from './Layout/Wrappers/pagesLayout.vue';

import { store } from './store/store';

Vue.config.productionTip = false;

Vue.use(BootstrapVue);
Vue.use(VueSessionStorage);

Vue.component('default-layout', Default);
Vue.component('userpages-layout', Pages);

new Vue({
  el: '#app',
  store: store,
  router,
  template: '<App/>',
  components: { App }
});
