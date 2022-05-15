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
                <div class="col-sm-6 col-md-3 col-xl-3">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-10 bg-warning"></div>
                            <i class="pe-7s-note2 text-white opacity-8"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Invention - In Progress</div>
                            <div class="widget-numbers">{{ inventionInProgressCount }}</div>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalInventionInprogress()" variant="warning" key="warning">Show Details</b-button>
                            </div>
                        </div>

                    </div>
                    <div class="divider m-0 d-md-none d-sm-block"></div>
                </div>
                <div class="col-sm-6 col-md-3 col-xl-3">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-9 bg-info"></div>
                            <i class="pe-7s-note2 text-white"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Utility Model - In Progress</div>
                            <div class="widget-numbers"><span>{{ utilityModelInProgressCount }}</span></div>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalUtilityModelInprogress()" variant="info" key="info">Show Details</b-button>
                            </div>
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
                <div class="col-sm-12 col-md-3 col-xl-3">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-9 bg-success"></div>
                            <i class="pe-7s-like2 text-white"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Finished Projects</div>
                            <div class="widget-numbers text-success"><span>{{ finishedProjectsCount }}</span></div>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalFinishedProjects()" variant="success" key="success">Show Details</b-button>
                            </div>
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
                <div class="col-sm-12 col-md-3 col-xl-3">
                    <div class="card no-shadow rm-border bg-transparent widget-chart text-left">
                        <div class="icon-wrapper rounded-circle">
                            <div class="icon-wrapper-bg opacity-9 bg-danger"></div>
                            <i class="pe-7s-loop text-white"></i>
                        </div>
                        <div class="widget-chart-content">
                            <div class="widget-subheading">Converted Inventions</div>
                            <div class="widget-numbers text-danger"><span>{{ convertedProjectsCount }}</span></div>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalConvertedProjects()" variant="danger" key="danger">Show Details</b-button>
                            </div>
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
                            {{ new Date().getFullYear() }} Project Summary
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
            <div class="col-md-12 col-xl-4">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-danger border-danger">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>{{ projectDue.officeActionDueToday }}</span>
                                            <small class="opacity-5"> Project</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due Today</h6>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalProjectsDueToday()" variant="danger" key="danger">Show Details</b-button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12 col-xl-4">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-warning border-warning">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>{{ projectDue.officeActionDueThisWeek }}</span>
                                            <small class="opacity-5"> Projects</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due This Week</h6>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalProjectsDueWeek()" variant="warning" key="warning">Show Details</b-button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12 col-xl-4">
                <div class="card mb-3 widget-chart widget-chart2 text-left card-btm-border card-shadow-info border-info">
                    <div class="widget-chat-wrapper-outer">
                        <div class="widget-chart-content pt-3 pl-3 pb-1">
                            <div class="widget-chart-flex">
                                <div class="widget-numbers">
                                    <div class="widget-chart-flex">
                                        <div class="fsize-4">
                                            <span>{{ projectDue.officeActionDueThisMonth }}</span>
                                            <small class="opacity-5"> Projects</small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h6 class="widget-subheading mb-0 opacity-5">Due This Month</h6>
                            <div class="widget-description opacity-8 text-focus">
                                <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalProjectsDueMonth()" variant="info" key="info">Show Details</b-button>
                            </div>
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
                        <ul class="todo-list-wrapper list-group list-group-flush">
                            <li v-for="(rp, i) in recentProjects" :key="i" class="list-group-item">
                                <div class="todo-indicator bg-warning"></div>
                                <div class="widget-content p-0">
                                    <div class="widget-content-wrapper">
                                        <div class="widget-content-left">
                                            <div class="widget-heading">
                                                {{ rp.projectTitle }}
                                                <!--<div class="badge badge-primary ml-2">{{ rp }}</div>-->
                                            </div>
                                            <div class="widget-subheading"><i>Applicant: {{ rp.applicantName + ' - ' + rp.applicationNo }}</i></div>

                                        </div>
                                    </div>
                                </div>
                            </li>
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
                                                <img width="42" class="rounded" src="@/assets/images/avatars/user-icon-green.png"
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
                        <a v-if="this.userInfoSession.userRoleId == 1" class="btn btn-primary" href="../admin/manage-users/user-register">Add Agent</a>
                    </div>
                </div>
            </div>
        </div>
        <b-modal ref="invention-inprogress" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-warning">Invention - In Progress</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-for="(ip, i) in inventionInProgressList" :key="i" class="dot-warning vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">{{ ip.projectTitle }}</h4>
                                    <p></p>
                                    <p><router-link class="text-warning" :to="{ name: 'project-detail', query: { projectId: ip.projectId } }">{{ ip.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " +ip.applicationNo}}</router-link> </p>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalInventionInprogress">Close</b-button>
        </b-modal>

        <b-modal ref="utility-model-inprogress" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-info">Utility Model - In Progress</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-for="(um, i) in utilityModelInProgressList" :key="i" class="dot-info vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">{{ um.projectTitle }}</h4>
                                    <p></p>
                                    <p><router-link class="text-info" :to="{ name: 'project-detail', query: { projectId: um.projectId } }">{{ um.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + um.applicationNo}}</router-link> </p>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalUtilityModelInprogress">Close</b-button>
        </b-modal>

        <b-modal ref="finished-projects-inprogress" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-success">Finished Projects</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-for="(fp, i) in finishedProjectsList" :key="i" class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">{{ fp.projectTitle }}</h4>
                                    <p></p>
                                    <p><router-link class="text-success" :to="{ name: 'project-detail', query: { projectId: fp.projectId } }">{{ fp.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + fp.applicationNo}}</router-link> </p>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalFinishedProjects">Close</b-button>
        </b-modal>

        <b-modal ref="converted-projects-inprogress" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-danger">Converterted Inventions</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-for="(cv, i) in convertedProjectsList" :key="i" class="dot-danger vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">{{ cv.projectTitle }}</h4>
                                    <p></p>
                                    <p><router-link class="text-danger" :to="{ name: 'project-detail', query: { projectId: cv.projectId } }">{{ cv.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + cv.applicationNo}}</router-link> </p>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalConvertedProjects">Close</b-button>
        </b-modal>

        <b-modal ref="projects-due-month" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-info">Due this Month</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-if="projectDueThisMonth.length > 0">
                            <div v-for="(dm, i) in projectDueThisMonth" :key="i" class="dot-info vertical-timeline-element">
                                <div>
                                    <span class="vertical-timeline-element-icon bounce-in"></span>
                                    <div class="vertical-timeline-element-content bounce-in">
                                        <h4 class="timeline-title">{{ dm.projectTitle }}</h4>
                                        <p></p>
                                        <p><router-link class="text-info" :to="{ name: 'project-detail', query: { projectId: dm.projectId } }">{{ dm.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + dm.applicationNo}}</router-link> </p>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="text-center"><p v-if="projectDueThisMonth.length == 0">There is no office action with due this month.</p></div>
                    </div>
                    </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalProjectsDueMonth">Close</b-button>
        </b-modal>

        <b-modal ref="projects-due-week" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-warning">Due this Week</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-if="projectDueThisWeek.length > 0">
                            <div v-for="(dw, i) in projectDueThisWeek" :key="i" class="dot-warning vertical-timeline-element">
                                <div>
                                    <span class="vertical-timeline-element-icon bounce-in"></span>
                                    <div class="vertical-timeline-element-content bounce-in">
                                        <h4 class="timeline-title">{{ dw.projectTitle }}</h4>
                                        <p></p>
                                        <p><router-link class="text-warning" :to="{ name: 'project-detail', query: { projectId: dw.projectId } }">{{ dw.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + dw.applicationNo}}</router-link> </p>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="text-center"><p v-if="projectDueThisWeek.length == 0">There is no office action with due this week.</p></div>
                    </div>
                    </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalProjectsDueWeek">Close</b-button>
        </b-modal>

        <b-modal ref="projects-due-today" hide-header hide-footer title="Warning" size="md">
            <div class="page-title-icon d-flex justify-content-center">
                <!--<i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />-->
                <h4 class="text-danger">Due Today</h4><hr />
            </div>

            <div class="scroll-area-lg">
                <!--<VuePerfectScrollbar class="scrollbar-container" v-once>-->
                <div class="p-4">
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div v-if="projectDueToday.length > 0">
                            <div v-for="(dt, i) in projectDueToday" :key="i" class="dot-danger vertical-timeline-element">
                                <div>
                                    <span class="vertical-timeline-element-icon bounce-in"></span>
                                    <div class="vertical-timeline-element-content bounce-in">
                                        <h4 class="timeline-title">{{ dt.projectTitle }}</h4>
                                        <p></p>
                                        <p><router-link class="text-danger" :to="{ name: 'project-detail', query: { projectId: dt.projectId } }">{{ dt.applicationTypeId == 1 ? "Invention" : "Utility Model"}}{{ " " + dt.applicationNo}}</router-link> </p>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="text-center"><p v-if="projectDueToday.length == 0">There is no office action with due today.</p></div>
                    </div>
                </div>
                <!--</VuePerfectScrollbar>-->
            </div>
            <b-button class="mt-2" variant="outline-secondary" block @click="toggleModalProjectsDueToday">Close</b-button>
        </b-modal>

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
                convertedProjectsCount: 0,
                latestOfficeActionUpdate: [],
                recentProjects: [],
                recentAgents: [],
                projectDue: [],
                inventionInProgressList: [],
                utilityModelInProgressList: [],
                finishedProjectsList: [],
                convertedProjectsList: [],
                projectDueThisMonth: [],
                projectDueThisWeek: [],
                projectDueToday: []
            }
        },

        methods: {
            hideModalInventionInProgress() {
                this.$refs['invention-inprogress'].hide()
            },
            toggleModalInventionInprogress() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['invention-inprogress'].toggle('#toggle-btn')
            },
            hideModalUtilityModelInProgress() {
                this.$refs['utility-model-inprogress'].hide()
            },
            toggleModalUtilityModelInprogress() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['utility-model-inprogress'].toggle('#toggle-btn')
            },
            hideModalFinishedProjects() {
                this.$refs['finished-projects-inprogress'].hide()
            },
            toggleModalFinishedProjects() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['finished-projects-inprogress'].toggle('#toggle-btn')
            },
            hideModalConvertedProjects() {
                this.$refs['converted-projects-inprogress'].hide()
            },
            toggleModalConvertedProjects() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['converted-projects-inprogress'].toggle('#toggle-btn')
            },
            hideModalProjectsDueMonth() {
                this.$refs['projects-due-month'].hide()
            },
            toggleModalProjectsDueMonth() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['projects-due-month'].toggle('#toggle-btn')
            },
            hideModalProjectsDueWeek() {
                this.$refs['projects-due-week'].hide()
            },
            toggleModalProjectsDueWeek() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['projects-due-week'].toggle('#toggle-btn')
            },
            hideModalProjectsDueToday() {
                this.$refs['projects-due-today'].hide()
            },
            toggleModalProjectsDueToday() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['projects-due-today'].toggle('#toggle-btn')
            },
            getInventionCount() {
                DashboardDataService.GetInventionCount(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.inventionInProgressCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getUtilityModelCount() {
                DashboardDataService.GetUtilityModelCount(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.utilityModelInProgressCount = response.data;
                        console.log("utilityModelInProgressCount");
                        console.log(this.utilityModelInProgressCount);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getFinishedProjectsCount() {
                DashboardDataService.GetFinishedProjectsCount(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.finishedProjectsCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getConvertedProjectsCount() {
                DashboardDataService.GetConvertedProjectsCount(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.convertedProjectsCount = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getInventionFinishedCount() { },
            getUtilityModelFinishedCount() { },
            //
            getInventionInProgress() {
                DashboardDataService.GetInventionInProgress(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.inventionInProgressList = response.data;
                        console.log("getInventionInProgress");
                        console.log(this.inventionInProgressList);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getUtilityModelInProgress() {
                DashboardDataService.GetUtilityModelInProgress(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.utilityModelInProgressList = response.data;
                        
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getFinishedProjects() {
                DashboardDataService.GetFinishedProjects(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.finishedProjectsList = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getConvertedProjects() {
                DashboardDataService.GetConvertedProjects(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.convertedProjectsList = response.data;
                        console.log("getConvertedProjects");
                        console.log(this.convertedProjectsList);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            //
            getOfficeActionUpdateList() {
                DashboardDataService.GetOfficeActionUpdateList(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.latestOfficeActionUpdate = response.data;
                        
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getRecentProjects() {
                DashboardDataService.GetRecentProjects(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
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
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getProjectWithDueCount() {
                DashboardDataService.GetProjectWithDueCount(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.projectDue = response.data;
                        console.log("GetProjectWithDueCount");
                        console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getProjectWithDueMonth() {
                DashboardDataService.GetProjectWithDueMonth(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.projectDueThisMonth = response.data;
                        console.log("getProjectWithDueMonth");
                        console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getProjectWithDueWeek() {
                DashboardDataService.GetProjectWithDueWeek(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.projectDueThisWeek = response.data;
                        console.log("getProjectWithDueWeek");
                        console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            getProjectWithDueToday() {
                DashboardDataService.GetProjectWithDueToday(this.userInfoSession.ipdmsUserId, this.userInfoSession.userRoleId)
                    .then(response => {
                        this.projectDueToday = response.data;
                        console.log("getProjectWithDueToday");
                        console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },




        },
        beforeMount() {
            this.userInfoSession = JSON.parse(sessionStorage.getItem('userInfo'));
            this.getInventionCount();
            this.getUtilityModelCount();
            this.getFinishedProjectsCount();
            this.getConvertedProjectsCount();
            this.getOfficeActionUpdateList();
            this.getRecentProjects();
            this.getAgents();
            this.getProjectWithDueCount();
            this.getInventionInProgress();
            this.getUtilityModelInProgress();
            this.getFinishedProjects();
            this.getConvertedProjects();
            this.getProjectWithDueMonth();
            this.getProjectWithDueWeek();
            this.getProjectWithDueToday();
        }

    }


</script>
