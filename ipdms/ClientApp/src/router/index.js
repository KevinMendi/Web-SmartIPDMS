import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

export default new Router({
    scrollBehavior() {
        return window.scrollTo({ top: 0, behavior: 'smooth' });
    },
    mode: 'history',
    routes: [

        // Homepage

        {
        /*path: '/dashboard',*/
            path: '/dashboard',
            name: 'dashboard',
            component: () => import('../Pages/Dashboards/Analytics.vue'),
        },
        {
            path: '/register/register-project',
            name: 'register-project',
            component: () => import('../Pages/Dashboards/RegisterProject.vue'),
        },
        {
            path: '/upload/upload-file',
            name: 'upload-file',
            component: () => import('../Pages/Uploads/UploadFile.vue'),
        },
        // Monitoring
        {
            path: '/monitoring/file-manager',
            name: 'file-manager',
            component: () => import('../Pages/Monitoring/FileManager.vue'),
        },
        {
            path: '/monitoring/chart',
            name: 'chart',
            component: () => import('../Pages/Monitoring/Chart.vue'),
        },
        {
            path: '/monitoring/file-manager/project-detail',
            name: 'project-detail',
            component: () => import('../Pages/Monitoring/ProjectDetail.vue'),
            props(route) {
                return { projectId: route.query.projectId }
            }
        },


        // Pages

        {
        /*path: '/pages/login-boxed',*/
         
            path: '/',
            name: 'login-boxed',
            meta: {layout: 'userpages'},
            component: () => import('../Pages/UserPages/LoginBoxed.vue'),
        },
        {
            path: '/pages/register-boxed',
            name: 'register-boxed',
            meta: {layout: 'userpages'},
            component: () => import('../Pages/UserPages/RegisterBoxed.vue'),
        },
        {
            path: '/pages/forgot-password-boxed',
            name: 'forgot-password-boxed',
            meta: {layout: 'userpages'},
            component: () => import('../Pages/UserPages/ForgotPasswordBoxed.vue'),
        },

        // Elements

        {
            path: '/elements/buttons-standard',
            name: 'buttons-standard',
            component: () => import('../Pages/Elements/Buttons/Standard.vue'),
        },
        {
            path: '/elements/dropdowns',
            name: 'dropdowns',
            component: () => import('../Pages/Elements/Dropdowns.vue'),
        },
        {
            path: '/elements/icons',
            name: 'icons',
            component: () => import('../Pages/Elements/Icons.vue'),
        },
        {
            path: '/elements/badges-labels',
            name: 'badges',
            component: () => import('../Pages/Elements/Badges.vue'),
        },
        {
            path: '/elements/cards',
            name: 'cards',
            component: () => import('../Pages/Elements/Cards.vue'),
        },
        {
            path: '/elements/list-group',
            name: 'list-group',
            component: () => import('../Pages/Elements/ListGroups.vue'),
        },
        {
            path: '/elements/timelines',
            name: 'timeline',
            component: () => import('../Pages/Elements/Timeline.vue'),
        },
        {
            path: '/elements/utilities',
            name: 'utilities',
            component: () => import('../Pages/Elements/Utilities.vue'),
        },

        // Components

        {
            path: '/components/tabs',
            name: 'tabs',
            component: () => import('../Pages/Components/Tabs.vue'),
        },
        {
            path: '/components/accordions',
            name: 'accordions',
            component: () => import('../Pages/Components/Accordions.vue'),
        },
        {
            path: '/components/JqueryTable',
            name: 'datatable',
            component: () => import('../Pages/Components/JqueryTable.vue'),
        },
        {
            path: '/components/modals',
            name: 'modals',
            component: () => import('../Pages/Components/Modals.vue'),
        },
        {
            path: '/components/progress-bar',
            name: 'progress-bar',
            component: () => import('../Pages/Components/ProgressBar.vue'),
        },
        {
            path: '/components/tooltips-popovers',
            name: 'tooltips-popovers',
            component: () => import('../Pages/Components/TooltipsPopovers.vue'),
        },
        {
            path: '/components/carousel',
            name: 'carousel',
            component: () => import('../Pages/Components/Carousel.vue'),
        },
        {
            path: '/components/pagination',
            name: 'pagination',
            component: () => import('../Pages/Components/Pagination.vue'),
        },
        {
            path: '/components/maps',
            name: 'maps',
            component: () => import('../Pages/Components/Maps.vue'),
        },

        // Tables

        {
            path: '/tables/regular-tables',
            name: 'regular-tables',
            component: () => import('../Pages/Tables/RegularTables.vue'),
        },

        // Dashboard Widgets

        {
            path: '/widgets/chart-boxes-3',
            name: 'chart-boxes-3',
            component: () => import('../Pages/Widgets/ChartBoxes3.vue'),
        },

        // Forms

        {
            path: '/forms/controls',
            name: 'controls',
            component: () => import('../Pages/Forms/Elements/Controls.vue'),
        },
        {
            path: '/forms/layouts',
            name: 'layouts',
            component: () => import('../Pages/Forms/Elements/Layouts.vue'),
        },
        // Charts

        {
            path: '/charts/chartjs',
            name: 'chartjs',
            component: () => import('../Pages/Charts/Chartjs.vue'),
        },

        // Database

        {
            path: '/database/weatherforecast',
            name: 'database',
            component: () => import('../Pages/Testdb/WeatherDisplay.vue'),
        },
        {
            path: '/database/users',
            name: 'users',
            component: () => import('../Pages/Testdb/UserDisplay.vue'),
        },
    ]
})
