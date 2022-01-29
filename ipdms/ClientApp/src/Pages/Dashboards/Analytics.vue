<template>
    <div>
        <!--<page-title :heading=heading :subheading=subheading :icon=icon></page-title>-->
        <div class="mb-3 card">
            <div class="card-header-tab card-header">
                <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                    <i class="header-icon lnr-charts icon-gradient bg-happy-green"> </i>
                    Project Statistics
                </div>
            </div>
            <div class="no-gutters row">
                <div class="col-sm-6 col-md-4 col-xl-4">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-10 bg-warning"></div>
                            <i class="pe-7s-ribbon text-white opacity-8"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Invention - In Progress</div>
                            <div class="widget-numbers">{{ inventionInProgressCount }}</div>
                            <!--<div class="widget-description opacity-8 text-focus">
                                <div class="d-inline text-danger pr-1">
                                    <font-awesome-icon icon="angle-down" />
                                    <span class="pl-1">54.1%</span>
                                </div>
                                out of total projects
                            </div>-->
                        </div>
                    </div>
                    <div class="divider m-0 d-md-none d-sm-block"></div>
                </div>
                <div class="col-sm-6 col-md-4 col-xl-4">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-9 bg-danger"></div>
                            <i class="pe-7s-tools text-white"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Utility Model - In Progress</div>
                            <div class="widget-numbers"><span>{{ utilityModelInProgressCount }}</span></div>
                            <!--<div class="widget-description opacity-8 text-focus">
                                Convert Rate:
                                <span class="text-info pl-1">
                                    <font-awesome-icon icon="angle-up" />
                                    <span class="pl-1">14.1%</span>
                                </span>
                            </div>-->
                        </div>
                    </div>
                    <div class="divider m-0 d-md-none d-sm-block"></div>
                </div>
                <div class="col-sm-12 col-md-4 col-xl-4">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-9 bg-success"></div>
                            <i class="pe-7s-timer text-white"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Finished Projects</div>
                            <div class="widget-numbers text-success"><span>{{ finishedProjectsCount }}</span></div>
                            <!--<div class="widget-description text-focus">
                                Increased by
                                <span class="text-warning pl-1">
                                    <font-awesome-icon icon="angle-up" />
                                    <span class="pl-1">7.35%</span>
                                </span>
                            </div>-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-lg-6">
                <div class="mb-3 card">
                    <div class="card-header-tab card-header">
                        <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                            <i class="header-icon lnr-cloud-download icon-gradient bg-happy-itmeo"> </i>
                            {Year} Project Summary
                        </div>
                    </div>
                    <div class="p-0 card-body">
                        <div class="p-1 slick-slider-sm mx-auto">
                            <div class="widget-chart widget-chart2 text-left p-0">
                                <div class="widget-chat-wrapper-outer">
                                    <div class="widget-chart-wrapper he-auto opacity-10 m-0">
                                        <bar></bar>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="d-block text-center card-footer">
                            <!--<a class="btn-shadow btn-wide btn-pill btn btn-focus" href="../monitoring/file-manager">
                                View Projects
                            </a>-->
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-lg-6">
                <div class="card-hover-shadow-2x mb-3 card">
                    <div class="card-header-tab card-header">
                        <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                            <i class="header-icon lnr-lighter icon-gradient bg-amy-crisp"> </i>
                            Latest Office Action Updates
                        </div>
                    </div>
                    <div class="scroll-area-lg">
                        <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                            <div class="p-4">
                                <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                                    <div v-for="(oa, i) in latestOfficeActionUpdate" :key="i" class="dot-primary vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">{{ oa.document.officeAction }}</h4>
                                                <p>{{ oa.project.appType + ' ' + oa.project.appNumber}}</p>
                                            </div>
                                       
                                        </div>
                                    </div>
                                    <!--<div class="dot-warning vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">Formality Examination Report</h4>
                                                <p> Project Title 3: this is sample </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dot-primary vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">
                                                    Acknowledgement
                                                </h4>
                                                <p>Sample 1: Project Title </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dot-danger vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">
                                                    Substantive Examination Report
                                                </h4>
                                                <p>Sample 1: Project Title </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dot-primary vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">
                                                    Acknowledgement
                                                </h4>
                                                <p> Sample Project Title 3</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dot-info vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title">Notice of Issuance of Certificate</h4>
                                                <p>Sample Project Title here</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="dot-dark vertical-timeline-element">
                                        <div>
                                            <span class="vertical-timeline-element-icon bounce-in"></span>
                                            <div class="vertical-timeline-element-content bounce-in">
                                                <h4 class="timeline-title"> Notice of Withdrawn Application </h4>
                                                <p> Sample Project Title 7</p>
                                            </div>
                                        </div>
                                    </div>-->
                                </div>
                            </div>
                        <!--</VuePerfectScrollbar>-->
                    </div>
                    <div class="d-block text-center card-footer">
                        <!--<button class="btn-shadow btn-wide btn-pill btn btn-focus">
                            Upload File
                        </button>-->
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-xl-3">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-success border-success">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>1</span>
                                            <small class="opacity-5"> Project</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due Today</h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-xl-3">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-primary border-primary">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>100</span>
                                            <small class="opacity-5"> Projects</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due This Week</h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-xl-3">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-warning border-warning">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>10</span>
                                            <small class="opacity-5"> Projects</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due This Month</h6>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-xl-3">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-danger border-danger">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>1000</span>
                                            <small class="opacity-5"> Projects</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due Last Month</h6>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-lg-6">
                <div class="card-hover-shadow-2x mb-3 card">
                    <div class="card-header-tab card-header">
                        <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                            <i class="header-icon lnr-database icon-gradient bg-malibu-beach"> </i>Recent Projects Registered
                        </div>
                    </div>
                    <div class="scroll-area-lg">
                        <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                            <ul  class="todo-list-wrapper list-group list-group-flush">
                                <li  v-for="(rp, i) in recentProjects" :key="i" class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    {{ rp.applicationNo }}
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Applicant: {{ rp.applicantName + ' - ' + rp.applicationNo }}</i></div>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <!--<li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                                <li class="list-group-item">
                                    <div class="todo-indicator bg-warning"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left">
                                                <div class="widget-heading">
                                                    Project Title Sample 1
                                                    <div class="badge badge-primary ml-2">Acknowledge</div>
                                                </div>
                                                <div class="widget-subheading"><i>Owner: John Doe</i></div>
                                            </div>
                                        </div>
                                    </div>
                                </li>-->
                            </ul>
                        <!--</VuePerfectScrollbar>-->
                    </div>
                    <div class="d-block text-right card-footer">
                        <a class="btn btn-primary" href="../monitoring/file-manager">View Projects</a>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-lg-6">
                <div class="card-hover-shadow-2x mb-3 card">
                    <div class="card-header-tab card-header">
                        <div class="card-header-title font-size-lg text-capitalize font-weight-normal">
                            <i class="header-icon lnr-database icon-gradient bg-malibu-beach"> </i>Recent Agents Registered
                        </div>
                    </div>
                    <div class="scroll-area-lg">
                        <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                            <ul class="todo-list-wrapper list-group list-group-flush">
                                <li v-for="(ra, i) in recentAgents" :key="i" class="list-group-item">
                                    <div class="todo-indicator bg-info"></div>
                                    <div class="widget-content p-0">
                                        <div class="widget-content-wrapper">
                                            <div class="widget-content-left mr-2">
                                            </div>
                                            <div class="widget-content-left mr-3">
                                                <div class="widget-content-left">
                                                    <img width="42" class="rounded" src="@/assets/images/avatars/1.jpg"
                                                         alt="">
                                                </div>
                                            </div>
                                            <div class="widget-content-left">
                                                
                                                <div class="widget-heading">{{ ra.firstname + ' ' + ra.lastname }}</div>
                                                <div class="widget-subheading">
                                                    {{ ra.email }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </li>
                             
                            </ul>
                        <!--</VuePerfectScrollbar>-->
                    </div>
                    <div class="d-block text-right card-footer">
                        <button class="btn btn-primary">Add New Agent</button>
                    </div>
                </div>
            </div>
        </div>

    </div>

</template>

<script>

    /*    import PageTitle from "../../Layout/Components/PageTitle.vue";*/
/*    import VuePerfectScrollbar from 'vue-perfect-scrollbar';*/
    import bar from '../../Pages/Charts/Chartjs/Bar';
    import DashboardDataService from "../../Services/DashboardDataService";

    // import chart1 from './Analytics/chart1';
    // import chart2 from './Analytics/chart2';
    // import chart3 from './Analytics/chart3';

    import { library } from '@fortawesome/fontawesome-svg-core'
    import {
        faTrashAlt,
        faCheck,
        faCalendarAlt,
        faAngleDown,
        faAngleUp,
        faTh,
    } from '@fortawesome/free-solid-svg-icons'
    //import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

    library.add(
        faTrashAlt,
        faCheck,
        faAngleDown,
        faAngleUp,
        faTh,
        faCalendarAlt,
    );

    export default {
        components: {
            // PageTitle,
            bar,
/*            VuePerfectScrollbar,*/
            //'font-awesome-icon': FontAwesomeIcon,
            // chart1,

            // chart2,
            // chart3,

        },
        data() {
            return {
                heading: 'Analytics Dashboard',
                subheading: 'This is an example dashboard created using build-in elements and components.',
                icon: 'pe-7s-plane icon-gradient bg-tempting-azure',

                userInfoSession: null,
                inventionInProgressCount: 0,
                utilityModelInProgressCount: 0,
                finishedProjectsCount: 0,
                latestOfficeActionUpdate: [],
                recentProjects: [],
                recentAgents:[]
            }
        },

        methods: {
            getInventionCount() {
                DashboardDataService.GetInventionCount(this.userInfoSession.ipdmsUserId)
                    .then(response => {
                        this.inventionInProgressCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getUtilityModelCount() {
                DashboardDataService.GetUtilityModelCount(this.userInfoSession.ipdmsUserId)
                    .then(response => {
                        this.utilityModelInProgressCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getFinishedProjectsCount() {
                DashboardDataService.GetFinishedProjectsCount(this.userInfoSession.ipdmsUserId)
                    .then(response => {
                        this.finishedProjectsCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getInventionFinishedCount() { },
            getUtilityModelFinishedCount() { },
            getOfficeActionUpdateList() {
                DashboardDataService.GetOfficeActionUpdateList(this.userInfoSession.ipdmsUserId)
                    .then(response => {
                        this.latestOfficeActionUpdate = response.data;
                        console.log("GetOfficeActionUpdateList");
                        console.log(response.data);
                        //console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getRecentProjects() {
                DashboardDataService.GetRecentProjects(this.userInfoSession.ipdmsUserId)
                    .then(response => {
                        this.recentProjects = response.data;
                        
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getAgents() {
                DashboardDataService.GetAgents()
                    .then(response => {
                        this.recentAgents = response.data;
                        console.log("GetAgents");
                        console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            }


        },
        beforeMount() {
            this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));
            this.getInventionCount();
            this.getUtilityModelCount();
            this.getFinishedProjectsCount();
            this.getOfficeActionUpdateList();
            this.getRecentProjects();
            this.getAgents();
        }

    }


</script>
