import { createWebHistory, createRouter } from "vue-router";
import Login from "@/components/Login.vue";
import Counter from "@/components/Counter.vue";
import FetchData from "@/components/FetchData.vue";
import TestData from "@/components/TestData.vue";
import Register from "@/components/Register.vue";

const routes = [
    {
        path: "/",
        name: "Login",
        component: Login,
    },
    {
        path: "/Register",
        name: "Register",
        component: Register,
    },
    {
        path: "/Counter",
        name: "Counter",
        component: Counter,
    },
    {
        path: "/FetchData",
        name: "FetchData",
        component: FetchData,
    },
    {
        path: "/TestData",
        name: "FetchMovie",
        component: TestData,
    },

];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;