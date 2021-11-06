<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <b-alert show dismissible fade variant="danger"> Upcoming Office Action response submission deadline is on 08/09/2021! </b-alert>
        <div class="main-card mb-3 card">
            <div class="card-body">
                <div class="row">
                    <div class="col-sm-12 col-md-6 col-xl-6">
                        <div>
                            Status: <div class="mb-2 mr-2 badge badge-warning">Completion of Final Requirements</div>
                            <div class="badge badge-danger ml-2">DUE ON 6/23/2021</div>
                        </div>
                        <div>
                            Agent: <a href="javascript:void(0);" class="mb-2 mr-2 badge badge-light">Maria dela Cruz</a>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-6 col-xl-6 d-inline-flex p-2 d-flex justify-content-end">
                        <b-button class="mr-2 mb-2 border-0 btn-transition" variant="info"> + Upload </b-button>
                    </div>
                </div>

                <!-- Use dot-info : for office action that is no need for response
            dot-success : for Office Action that was successfully responded (has sumbit response date)
            dot-warning : when Office action response is not yet done, 2-3 weeks prior
            dot-danger : when 3 days or 0 days left before deadline of submission
            dot-dark : if it exceeds submission date-->
                <div>
                    <div class="divider"></div>
                    <div class="vertical-time-simple vertical-without-time vertical-timeline vertical-timeline--animate vertical-timeline--one-column">
                        <div class="dot-info vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Acknowledgement</h4>
                                    <p>
                                        Received on <span class="text-success">01/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">N/A</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">06/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">07/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">08/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">09/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">10/01/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">11/01/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-success vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">Formality Examination Report</h4>
                                    <p>
                                        Received on <span class="text-success">12/23/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">12/30/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="dot-warning vertical-timeline-element">
                            <div>
                                <span class="vertical-timeline-element-icon bounce-in"></span>
                                <div class="vertical-timeline-element-content bounce-in">
                                    <h4 class="timeline-title">
                                        Notice of Publication
                                        <div class="badge badge-danger ml-2">DUE ON 6/23/2021</div>
                                    </h4>
                                    <p>
                                        Received on <span class="text-success">12/23/2021 10:00 AM</span>
                                        Submitted response on <span class="text-success">12/30/2021 7:00 AM</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="row">
                        <div class="col-sm-12 col-md-6 col-xl-6 d-flex flex-column">
                            <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalFinish()" variant="success" key="success">Finish Project</b-button>
                        </div>
                        <div class="col-sm-12 col-md-6 col-xl-6 d-flex flex-column">
                            <b-button class="mr-2 mb-2 btn-hover-shine btn-transition" @click="toggleModalConvert()" variant="warning" key="warning">Convert to Utility Model</b-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <b-modal ref="utility-model" hide-header hide-footer title="Warning" size="sm">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                This project will be converted to Utility Model. Do you want to continue?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="hideModalConvert">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalConvert">No</b-button>
        </b-modal>
        <b-modal ref="finish-project" hide-header hide-footer title="Warning" size="sm">
            <div class="page-title-icon d-flex justify-content-center">
                <i class="pe-7s-attention icon-gradient bg-warm-flame fa-5x" />
            </div>
            <div class="d-block text-center">
                This project will be marked as finished. Do you want to continue?
            </div>
            <b-button class="mt-3" variant="outline-danger" block @click="hideModalFinish">Yes</b-button>
            <b-button class="mt-2" variant="outline-warning" block @click="toggleModalFinish">No</b-button>
        </b-modal>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";

    export default {
        components: {
            PageTitle,

        },
        data() {
            return {
                heading: 'Smart Intellectual Property Document Management System',
                subheading: 'Short project description here ',
                icon: 'pe-7s-portfolio icon-gradient bg-happy-itmeo',
            }
        },
        methods: {

            hideModalConvert() {
                this.$refs['utility-model'].hide()
            },
            toggleModalConvert() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['utility-model'].toggle('#toggle-btn')
            },

            hideModalFinish() {
                this.$refs['finish-project'].hide()
            },
            toggleModalFinish() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['finish-project'].toggle('#toggle-btn')
            },
        }
    }
</script>
