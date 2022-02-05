<template>
    <div>
        <form @submit.prevent="updateProject">
            <div class="submit-form">
                <!--<div class="h-100" style="background-color: #38d39f;">-->
                <!--<div class="d-flex h-100 justify-content-center align-items-center">-->
                <!--<b-col md="8" class="mx-auto app-login-box">-->
                <!--<div class=" mx-auto mb-3" />-->
                <!--<div class="modal-dialog w-100">-->
                <div class="modal-content">
                    <div class="modal-body">
                        <b-alert show :variant="ipdmsVariant" dismissible fade v-model="showAlert">
                            {{alertMessage}}
                        </b-alert>
                        <h4 class="mt-2">
                            <div>Project Details</div>

                        </h4>
                        <div class="divider" />
                        <div class="row">
                            <div class="col-md-6">
                                <label for="applicationType" class="">Application Type</label>
                                <select class="mb-2 form-control" name="applicationType" id="applicationType" v-model="project.appTypeId" v-if="action == 'r'" readonly>
                                    <option :value="project.appTypeId">{{ project.appTypeId == 1 ? "Invention" : "Utility Model" }}</option>
                                </select>
                                <select class="mb-2 form-control" name="applicationType" id="applicationType" v-model="project.appTypeId" v-if="action == 'e'">
                                    <option value="-1" disabled>Please select *</option>
                                    <option value="1">Invention</option>
                                    <option value="2">Utility Model</option>
                                </select>
                            </div>
                            <div class="col-md-6">
                                <div class="position-relative form-group">
                                    <label for="applicationNumber" class="">Application Number</label>
                                    <input name="applicationNumber" id="applicationNumber" placeholder="Application Number" type="text" class="form-control" v-model="project.appNumber" v-if="action == 'r'" readonly required>
                                    <input name="applicationNumber" id="applicationNumber" placeholder="Application Number" type="text" class="form-control" v-model="project.appNumber" v-if="action == 'e'"  required>
                                </div>
                            </div>
                        </div>
                        <label for="projectTitle" class="">Project Title</label>
                        <b-form-group id="exampleInputGroup1"
                                      label-for="exampleInput1">
                            <b-form-input id="projectTitle"
                                          v-if="action == 'r'" readonly
                                          name="projectTitle"
                                          type="text"
                                          v-model="project.projectTitle"
                                          required
                                          placeholder="Project title *">
                            </b-form-input>
                            <b-form-input id="projectTitle"
                                          v-if="action == 'e'" 
                                          name="projectTitle"
                                          type="text"
                                          v-model="project.projectTitle"
                                          required
                                          placeholder="Project title *">
                            </b-form-input>
                        </b-form-group>

                        <div class="row">
                            <div class="col-md-6">
                                <label for="applicantName" class="">Applicant Name</label>
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="applicantName"
                                                  v-if="action == 'r'" readonly
                                                  name="applicantName"
                                                  type="text"
                                                  v-model="project.applicantName"
                                                  required
                                                  placeholder="Applicant Name *">
                                    </b-form-input>

                                    <b-form-input id="applicantName"
                                                  v-if="action == 'e'" 
                                                  name="applicantName"
                                                  type="text"
                                                  v-model="project.applicantName"
                                                  required
                                                  placeholder="Applicant Name *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-6">
                                
                            </div>
                        </div>
                        <!--<b-form-checkbox name="check" id="exampleCheck">
        Accept our <a href="javascript:void(0);">Terms and Conditions</a>.
    </b-form-checkbox>-->
                        <div class="divider" />
                        <!--<h6 class="mb-0">
        Already have an account?
        <router-link :to="{ name: 'login-boxed' }" class="text-primary">Sign in</router-link>
    </h6>-->
                    </div>
                    <div class="modal-footer d-block text-center">
                        <router-link :to="{ name: 'manage-projects' }">
                            <b-button variant="warning" class="btn-wide btn-pill btn-shadow btn-hover-shine float-left"
                                      size="lg">Back</b-button>
                        </router-link>
                        <b-button type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine float-right" v-if="action == 'e'" readonly
                                  size="lg">
                            Update Project
                        </b-button>
                    </div>
                </div>
                <!--</div>-->
                <div class="text-center text-white opacity-8 mt-3">
                    Copyright &copy; IPDMS 2021
                </div>
                <!--</b-col>-->
                <!--</div>-->
                <!--</div>-->
            </div>
        </form>
    </div>
</template>

<script>
    import FileDataService from "../../Services/FileDataService";
    export default {
        name: "ProjectIno",
        props: {
            projectId: {
                type: Number,
                required: true
            },
            action: {
                //0 - read
                //1 - edit
                type: String,
                required: true
            }

        },
        data() {
            return {
                project: {
                    appTypeId: -1,
                    appNumber: "",
                    applicantName: "",
                    projectTitle: "",
                    projectStatusId: 0,
                    agentId: 0,
                    createUserId: 0,
                    lastUpdateUserId: 0
                },
                submitted: false,
                showAlert: false,
                alertMessage: "",
                ipdmsVariant: 'success'
            };
        },
        methods: {
            updateProject() {
                var data = {
                    projectId: this.projectId,
                    applicationTypeId: this.project.appTypeId,
                    projectStatusId: this.project.projectStatusId,
                    ipdmsUserId: this.project.agentId,
                    applicationNo: this.project.appNumber.trim(),
                    projectTitle: this.project.projectTitle.trim(),
                    applicantName: this.project.applicantName.trim(),
                    createUserId: this.project.createUserId,
                    lastUpdateUserId: this.project.lastUpdateUserId,
                    isDeleted: false
                };

                FileDataService.UpdateProject(this.projectId, data)
                        .then(response => {
                            console.log(response.data);
                                this.alertMessage = "Successfuly Updated!";
                                this.delayedAlert();
                        })
                        .catch(e => {
                            this.alertMessage = e;
                            this.delayedAlert();
                        });
            },

            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 3000);
            },
            getProjectById() {
                FileDataService.GetProjectById(this.projectId)
                    .then(response => {
                        var data = response.data;
                        console.log("data");
                        console.log(data);
                        this.project.appTypeId = data.project.appTypeId;
                        this.project.appNumber = data.project.appNumber;
                        this.project.applicantName = data.project.applicantName;
                        this.project.projectTitle = data.project.projectTitle;
                        this.project.projectStatusId = data.project.projectStatusId;
                        this.project.agentId = data.project.agentId;
                        this.project.createUserId = data.project.createUserId;
                        this.project.lastUpdateUserId = data.project.lastUpdateUserId;

                        console.log("project");
                        console.log(this.project);

                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });
            }
        },
        beforeMount() {
            this.getProjectById();
            console.log(this.projectId);
            console.log(this.action);
        }
    }
</script>
