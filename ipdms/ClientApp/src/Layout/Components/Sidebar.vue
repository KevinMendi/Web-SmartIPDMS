<template>
    <div class="app-sidebar sidebar-shadow" @mouseover="toggleSidebarHover('add','closed-sidebar-open')" @mouseleave="toggleSidebarHover('remove','closed-sidebar-open')">
        <div class="app-header__logo">
            <!--<div class="logo-src"/>-->
            <img src="../../assets/images/logo-with-name.png" width="223.5" />
            <!--<h4>Smart IPDMS</h4>-->
            <div class="header__pane ml-auto">
                <button type="button" class="hamburger close-sidebar-btn hamburger--elastic" v-bind:class="{ 'is-active' : isOpen }" @click="toggleBodyClass('closed-sidebar')">
                    <span class="hamburger-box">
                        <span class="hamburger-inner"></span>
                    </span>
                </button>
            </div>
        </div>
        <div v-if="userInfoSession.userRoleId == 3" class="app-sidebar-content">
            <VuePerfectScrollbar class="app-sidebar-scroll" v-once>
                <sidebar-menu showOneChild :menu="menu" />
            </VuePerfectScrollbar>
        </div>
        <div v-if="userInfoSession.userRoleId == 1 || userInfoSession.userRoleId == 2" class="app-sidebar-content">
            <VuePerfectScrollbar class="app-sidebar-scroll" v-once>
                <sidebar-menu showOneChild :menu="menuAdmin" />
            </VuePerfectScrollbar>
        </div>

    </div>
</template>

<script>
    import {SidebarMenu} from 'vue-sidebar-menu'
    import VuePerfectScrollbar from 'vue-perfect-scrollbar'

    export default {
        components: {
            SidebarMenu,
            VuePerfectScrollbar
        },
        data() {
            return {
                isOpen: false,
                sidebarActive: false,

                menu: [
                    {
                        header: true,
                        title: 'Home',
                    },
                    {
                        title: 'Dashboards',
                        icon: 'pe-7s-rocket',
                        href: '/dashboard',
                        //child: [
                        //    {
                        //        href: '/',
                        //        title: 'Analytics',
                        //    },
                        //]
                    },
                    {
                        icon: 'pe-7s-plus',
                        title: 'Register Project',
                        href: '/register/register-project',
                    },
                    {
                        icon: 'pe-7s-cloud-upload',
                        title: 'Upload File',
                        href: '/upload/upload-file',
                    },
                    {
                        icon: 'pe-7s-cloud-upload',
                        title: 'File Upload',
                        href: '/uploads/upload-file',
                    },

                    {
                        header: true,
                        title: 'MONITORING'
                    },
                    {
                        icon: 'pe-7s-folder',
                        title: 'File Manager',
                        href: '/monitoring/file-manager'
                    },

                    //{
                    //    header: true,
                    //    title: 'Users'
                    //},
                    //{
                    //    icon: 'pe-7s-users',
                    //    title: 'Manage Users',
                    //    href: '/admin/manage-users'
                    //},
                    //{
                    //    header: true,
                    //    title: 'Projects'
                    //},
                    //{
                    //    icon: 'pe-7s-folder',
                    //    title: 'Manage Projects',
                    //    href: '/admin/manage-projects'
                    //},
                    //{
                    //    header: true,
                    //    title: 'Documents'
                    //},
                    //{
                    //    icon: 'pe-7s-news-paper',
                    //    title: 'Manage Office Actions',
                    //    href: '/admin/manage-office-action'
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'Chart',
                    //    href: '/monitoring/chart'
                    //},

                    //{
                    //    title: 'Pages',
                    //    icon: 'pe-7s-browser',
                    //    child: [
                    //        {
                    //            href: '/pages/login-boxed',
                    //            title: 'Login Boxed',
                    //        },
                    //        {
                    //            href: '/pages/register-boxed',
                    //            title: 'Register Boxed',
                    //        },
                    //        {
                    //            href: '/pages/forgot-password-boxed',
                    //            title: 'Forgot Password Boxed',
                    //        },
                    //    ]
                    //},
                    /////////////////////////////////////////////////////////////UI COMPONENTS//////////////////////////////
                    //{
                    //    header: true,
                    //    title: 'UI Components',
                    //},
                    //{
                    //    icon: 'pe-7s-diamond',
                    //    title: 'Elements',
                    //    child: [
                    //        {
                    //            title: 'Buttons',
                    //            child: [
                    //                {
                    //                    title: 'Standard',
                    //                    href: '/elements/buttons-standard',
                    //                },
                    //            ]
                    //        },
                    //        {
                    //            title: 'Dropdowns',
                    //            href: '/elements/dropdowns',
                    //        },
                    //        {
                    //            title: 'Icons',
                    //            href: '/elements/icons',
                    //        },
                    //        {
                    //            title: 'Badges',
                    //            href: '/elements/badges-labels',
                    //        },
                    //        {
                    //            title: 'Cards',
                    //            href: '/elements/cards',
                    //        },
                    //        {
                    //            title: 'List Groups',
                    //            href: '/elements/list-group',
                    //        },
                    //        {
                    //            title: 'Timeline',
                    //            href: '/elements/timelines',
                    //        },
                    //        {
                    //            title: 'Utilities',
                    //            href: '/elements/utilities',
                    //        },
                    //    ],
                    //},
                    //{
                    //    icon: 'pe-7s-car',
                    //    title: 'Components',
                    //    child: [
                    //        {
                    //            title: 'Tabs',
                    //            href: '/components/tabs',
                    //        },
                    //        {
                    //            title: 'Accordions',
                    //            href: '/components/accordions',
                    //        },
                    //        {
                    //            title: 'Data Table',
                    //            href: '/components/JqueryTable',
                    //        },
                    //        {
                    //            title: 'Modals',
                    //            href: '/components/modals',
                    //        },
                    //        {
                    //            title: 'Progress Bar',
                    //            href: '/components/progress-bar',
                    //        },
                    //        {
                    //            title: 'Tooltips & Popovers',
                    //            href: '/components/tooltips-popovers',
                    //        },
                    //        {
                    //            title: 'Carousel',
                    //            href: '/components/carousel',
                    //        },
                    //        {
                    //            title: 'Pagination',
                    //            href: '/components/pagination',
                    //        },
                    //        {
                    //            title: 'Maps',
                    //            href: '/components/maps',
                    //        },
                    //    ],
                    //},
                    //{
                    //    icon: 'pe-7s-display2',
                    //    title: 'Tables',
                    //    child: [
                    //        {
                    //            title: 'Regular Tables',
                    //            href: '/tables/regular-tables',
                    //        },
                    //    ]
                    //},
                    //{
                    //    header: true,
                    //    title: 'Dashboard Boxes',
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'Chart Boxes',
                    //    href: '/widgets/chart-boxes-3',
                    //},
                    //{
                    //    header: true,
                    //    title: 'Forms',
                    //},
                    //{
                    //    icon: 'pe-7s-light',
                    //    title: 'Elements',
                    //    child: [
                    //        {
                    //            title: 'Controls',
                    //            href: '/forms/controls',
                    //        },
                    //        {
                    //            title: 'Layouts',
                    //            href: '/forms/layouts',
                    //        },
                    //    ],
                    //},
                    //{
                    //    header: true,
                    //    title: 'Charts',
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'ChartJS',
                    //    href: '/charts/chartjs',
                    //},
                    //{
                    //    header: true,
                    //    title: 'Database',
                    //},
                    //{
                    //    icon: 'pe-7s-server',
                    //    title: 'Collection',
                    //    href: '/database/weatherforecast',
                    //},
                    //{
                    //    icon: 'pe-7s-server',
                    //    title: 'Database',
                    //    href: '/database/users',
                    //},
                ],
                menuAdmin: [
                    {
                        header: true,
                        title: 'Home',
                    },
                    {
                        title: 'Dashboards',
                        icon: 'pe-7s-rocket',
                        href: '/dashboard',
                        //child: [
                        //    {
                        //        href: '/',
                        //        title: 'Analytics',
                        //    },
                        //]
                    },
                    {
                        icon: 'pe-7s-plus',
                        title: 'Register Project',
                        href: '/register/register-project',
                    },
                    {
                        icon: 'pe-7s-cloud-upload',
                        title: 'Upload File',
                        href: '/upload/upload-file',
                    },
                    //{
                    //    icon: 'pe-7s-cloud-upload',
                    //    title: 'File Upload',
                    //    href: '/uploads/upload-file',
                    //},

                    {
                        header: true,
                        title: 'MONITORING'
                    },
                    {
                        icon: 'pe-7s-folder',
                        title: 'File Manager',
                        href: '/monitoring/file-manager'
                    },

                    {
                        header: true,
                        title: 'Users'
                    },
                    {
                        icon: 'pe-7s-users',
                        title: 'Manage Users',
                        href: '/admin/manage-users'
                    },
                    {
                        header: true,
                        title: 'Projects'
                    },
                    {
                        icon: 'pe-7s-folder',
                        title: 'Manage Projects',
                        href: '/admin/manage-projects'
                    },
                    //{
                    //    header: true,
                    //    title: 'Documents'
                    //},
                    //{
                    //    icon: 'pe-7s-news-paper',
                    //    title: 'Manage Office Actions',
                    //    href: '/admin/manage-office-action'
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'Chart',
                    //    href: '/monitoring/chart'
                    //},

                    //{
                    //    title: 'Pages',
                    //    icon: 'pe-7s-browser',
                    //    child: [
                    //        {
                    //            href: '/pages/login-boxed',
                    //            title: 'Login Boxed',
                    //        },
                    //        {
                    //            href: '/pages/register-boxed',
                    //            title: 'Register Boxed',
                    //        },
                    //        {
                    //            href: '/pages/forgot-password-boxed',
                    //            title: 'Forgot Password Boxed',
                    //        },
                    //    ]
                    //},
                    /////////////////////////////////////////////////////////////UI COMPONENTS//////////////////////////////
                    //{
                    //    header: true,
                    //    title: 'UI Components',
                    //},
                    //{
                    //    icon: 'pe-7s-diamond',
                    //    title: 'Elements',
                    //    child: [
                    //        {
                    //            title: 'Buttons',
                    //            child: [
                    //                {
                    //                    title: 'Standard',
                    //                    href: '/elements/buttons-standard',
                    //                },
                    //            ]
                    //        },
                    //        {
                    //            title: 'Dropdowns',
                    //            href: '/elements/dropdowns',
                    //        },
                    //        {
                    //            title: 'Icons',
                    //            href: '/elements/icons',
                    //        },
                    //        {
                    //            title: 'Badges',
                    //            href: '/elements/badges-labels',
                    //        },
                    //        {
                    //            title: 'Cards',
                    //            href: '/elements/cards',
                    //        },
                    //        {
                    //            title: 'List Groups',
                    //            href: '/elements/list-group',
                    //        },
                    //        {
                    //            title: 'Timeline',
                    //            href: '/elements/timelines',
                    //        },
                    //        {
                    //            title: 'Utilities',
                    //            href: '/elements/utilities',
                    //        },
                    //    ],
                    //},
                    //{
                    //    icon: 'pe-7s-car',
                    //    title: 'Components',
                    //    child: [
                    //        {
                    //            title: 'Tabs',
                    //            href: '/components/tabs',
                    //        },
                    //        {
                    //            title: 'Accordions',
                    //            href: '/components/accordions',
                    //        },
                    //        {
                    //            title: 'Data Table',
                    //            href: '/components/JqueryTable',
                    //        },
                    //        {
                    //            title: 'Modals',
                    //            href: '/components/modals',
                    //        },
                    //        {
                    //            title: 'Progress Bar',
                    //            href: '/components/progress-bar',
                    //        },
                    //        {
                    //            title: 'Tooltips & Popovers',
                    //            href: '/components/tooltips-popovers',
                    //        },
                    //        {
                    //            title: 'Carousel',
                    //            href: '/components/carousel',
                    //        },
                    //        {
                    //            title: 'Pagination',
                    //            href: '/components/pagination',
                    //        },
                    //        {
                    //            title: 'Maps',
                    //            href: '/components/maps',
                    //        },
                    //    ],
                    //},
                    //{
                    //    icon: 'pe-7s-display2',
                    //    title: 'Tables',
                    //    child: [
                    //        {
                    //            title: 'Regular Tables',
                    //            href: '/tables/regular-tables',
                    //        },
                    //    ]
                    //},
                    //{
                    //    header: true,
                    //    title: 'Dashboard Boxes',
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'Chart Boxes',
                    //    href: '/widgets/chart-boxes-3',
                    //},
                    //{
                    //    header: true,
                    //    title: 'Forms',
                    //},
                    //{
                    //    icon: 'pe-7s-light',
                    //    title: 'Elements',
                    //    child: [
                    //        {
                    //            title: 'Controls',
                    //            href: '/forms/controls',
                    //        },
                    //        {
                    //            title: 'Layouts',
                    //            href: '/forms/layouts',
                    //        },
                    //    ],
                    //},
                    //{
                    //    header: true,
                    //    title: 'Charts',
                    //},
                    //{
                    //    icon: 'pe-7s-graph2',
                    //    title: 'ChartJS',
                    //    href: '/charts/chartjs',
                    //},
                    //{
                    //    header: true,
                    //    title: 'Database',
                    //},
                    //{
                    //    icon: 'pe-7s-server',
                    //    title: 'Collection',
                    //    href: '/database/weatherforecast',
                    //},
                    //{
                    //    icon: 'pe-7s-server',
                    //    title: 'Database',
                    //    href: '/database/users',
                    //},
                ],
                collapsed: true,

                windowWidth: 0,

                userInfoSession: null,

            }
        },
        props: {
            sidebarbg: String,
        },
        methods: {

            toggleBodyClass(className) {
                const el = document.body;
                this.isOpen = !this.isOpen;

                if (this.isOpen) {
                    el.classList.add(className);
                } else {
                    el.classList.remove(className);
                }
            },
            toggleSidebarHover(add, className) {
                const el = document.body;
                this.sidebarActive = !this.sidebarActive;

                this.windowWidth = document.documentElement.clientWidth;

                if (this.windowWidth > '992') {
                    if (add === 'add') {
                        el.classList.add(className);
                    } else {
                        el.classList.remove(className);
                    }
                }
            },
            getWindowWidth() {
                const el = document.body;

                this.windowWidth = document.documentElement.clientWidth;

                if (this.windowWidth < '1350') {
                    el.classList.add('closed-sidebar', 'closed-sidebar-md');
                } else {
                    el.classList.remove('closed-sidebar', 'closed-sidebar-md');
                }
            },
        },
        mounted() {
            this.$nextTick(function () {
                window.addEventListener('resize', this.getWindowWidth);

                //Init
                this.getWindowWidth()
            })
        },
        beforeMount() {
            this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));


            console.log("sdfsdfsdfsdfsdfsdfsdfsdfsdfsdfsdf");
            console.log(this.userInfoSession);
        },

        beforeDestroy() {
            window.removeEventListener('resize', this.getWindowWidth);
        }
    }
</script>
