<template>
    <div>
        
            <form @submit.prevent="saveProject" ref="registerProjectForm">
                <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
                <div class="submit-form">
                    <div class="content">
                        <div class="main-card mb-3 card">
                            <b-overlay :show="busy"
                                       rounded
                                       opacity="0.6"
                                       spinner-small
                                       spinner-variant="primary"
                                       class="d-inline-block"
                                       @hidden="onHidden">
                                <div class="card-body">
                                    <!--<h5 class="card-title">Grid Rows</h5>-->

                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="position-relative form-group">
                                                <label for="applicationType" class="">Application Type</label>
                                                <select name="applicationType" id="applicationType" class="form-control" v-model="project.applicationType" required>
                                                    <option v-for="(applicationType, index) in applicationTypes" :key="index" :value="applicationType.applicationTypeId">{{ applicationType.applicationTypeName }}</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="position-relative form-group">
                                                <label for="applicationNumber" class="">Application Number</label>
                                                <input name="applicationNumber" id="applicationNumber" placeholder="Application Number" type="text" class="form-control" v-model="project.applicationNumber" required>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label for="projectName"
                                               class="">Project Name</label><input name="projectName"
                                                                                   id="projectName"
                                                                                   placeholder="Project Name"
                                                                                   v-model="project.projectName"
                                                                                   type="text"
                                                                                   class="form-control"
                                                                                   required>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="position-relative form-group">
                                                <label for="officeAction" class="">Office Action:</label><select name="officeAction"
                                                                                                                 id="officeAction"
                                                                                                                 v-model="project.officeAction"
                                                                                                                 class="form-control" disabled>
                                                    <option value="1" selected="selected">Acknowledgement</option>
                                                    <option value="2">Formality Examination Report/Subsequent Formality Examination Report</option>
                                                    <option value="3">Notice of Publication</option>
                                                    <option value="4">Substantive Examination Report/Subsequent Substantive Report</option>
                                                    <option value="5">Completion of Final Requirements</option>
                                                    <option value="6">Notice of Allowance</option>
                                                    <option value="7">Certificate</option>
                                                    <option value="11">Notice of Issuance of Certificate</option>
                                                    <option value="12">Notice of Withdrawn Application</option>
                                                    <option value="13">Revival Order</option>
                                                    <option value="14">Notice of Forfeiture of Application</option>

                                                </select>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="position-relative form-group">
                                                <label for="filingDate"
                                                       class="">Document Filing Date</label><br />
                                                <date-picker v-model="project.fillingDate" valueType="format" format="DD/MM/YYYY" required></date-picker>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="position-relative form-group">
                                                <label for="filingDate"
                                                       class="">Mailing Date</label><br />
                                                <date-picker v-model="project.mailingDate" valueType="format" format="DD/MM/YYYY" required></date-picker>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                        <div class="col-md-6">
                                            <div class="position-relative form-group">
                                                <label for="applicantName"
                                                       class="">Applicant Name</label><input name="applicantName"
                                                                                             id="applicantName"
                                                                                             placeholder="Applicant Name"
                                                                                             v-model="project.applicantName"
                                                                                             type="text"
                                                                                             class="form-control"
                                                                                             required>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="position-relative form-group">
                                                <label for="agentName"
                                                       class="">Agent Name</label>
                                                <!--<input name="agentName"
                    id="agentName"
                    placeholder="Agent Name"
                    v-model="project.agentName"
                    type="text"
                    class="form-control"
                    required>-->
                                                <select name="agentName"
                                                        id="officeAction"
                                                        v-model="project.agentName"
                                                        class="form-control">
                                                    <option v-for="(agent, index) in agents" :key="index" :value="agent.ipdmsUserId">{{ agent.firstname + ' ' + agent.lastname  }}</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label for="uploadFile"
                                               class="">File</label><input name="file"
                                                                           id="uploadFile"
                                                                           type="file"
                                                                           @change="onFileChange"
                                                                           class="form-control-file"
                                                                           required>
                                        <small class="form-text text-muted">
                                            Please upload "Acknowledgement Office Action".
                                        </small>
                                    </div>
                                    <b-alert :class="error ? 'danger' : 'success'" show dismissible fade v-model="showAlert">
                                        {{alertMessage}}
                                    </b-alert><br />
                                    <button class="mt-2 btn btn-primary" :disabled="disable">Register Project</button>
                                    <!--<b-button type="submit" ref="show" :disabled="show" variant="primary" @click="show = true">
            Show overlay
        </b-button>-->
                                    <!--<b-button ref="show" :disabled="show" variant="primary" @click="show = true">
            Show overlay
        </b-button>-->
                                </div>

                            </b-overlay>
                        </div>
                    </div>
                </div>
            </form>
            
        
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import UserDataService from "../../Services/UserDataService";
    import LookUpDataService from "../../Services/LookUpDataService";
    import DatePicker from 'vue2-datepicker';
    import 'vue2-datepicker/index.css';

    export default {
        components: {
            PageTitle,
            DatePicker,

        },
        data: () => ({
            heading: 'Register Project',
            subheading: 'Register a Project by providing the details and uploading the Acknowledgement Office Action',
            icon: 'pe-7s-folder icon-gradient bg-happy-itmeo',
            busy: false,
            disable: false,

            counter: 99,
            max: 100,
            timer: null,
            striped: true,
            image: '',
            imageResult: '',
            project: {
                applicationType: 0,
                applicationNumber: "",
                projectName: "",
                officeAction: 1,
                fillingDate: null,
                mailingDate: null,
                applicantName: "",
                agentName: "",
                pdfBase64: "",
                fileName: "",
                
            },
            agents: [],
            applicationTypes:[],
            stringJSON: '',
            submitted: false,
            showAlert: false,
            alertMessage: "",
            error: null,
            user: null

        }),
        methods: {
            onShown() {
                // Focus the cancel button when the overlay is showing
                this.$refs.cancel.focus()
            },
            onHidden() {
                // Focus the show button when the overlay is removed
                this.$refs.show.focus()
            },
           
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                var fileInput = document.getElementById('uploadFile');
                this.project.fileName = fileInput.files[0].name;
                this.createImage(files[0]);
            },
            createImage(file) {
                //var data = {
                //    image64: this.image
                //}
                var reader = new FileReader();

                //reader.onload = (e) => {
                //    this.image = e.target.result;
                //    data.image64 = e.target.result;
              

                //};
                reader.onload = this.fileLoaded;

                reader.readAsDataURL(file);


            },
            fileLoaded(e) {
                this.project.pdfBase64 = e.target.result;
               
                //console.log("console.log(this.stringJSON);");
                //console.log(this.project);
            },
            analyzeImage() {
                FileDataService.AnalyzeImage(this.image)
                    .then(response => {
                        this.imageResult = response.data;
                    })
                    .catch(e => {
                        this.imageResult = e;
                    });
            },
            saveProject() {
                this.busy = true;
                this.disable = true;
                var data = {
                    applicationTypeId: this.project.applicationType,
                    applicationNo: (this.project.applicationNumber).trim(),
                    projectTitle: (this.project.projectName).trim(),
                    officeActionId: this.project.officeAction,
                    fillingDate: this.project.fillingDate,//add
                    mailingDate: this.project.mailingDate,
                    applicantName: (this.project.applicantName).trim(),
                    agentName: this.project.agentName,//add
                    pdfBase64: this.project.pdfBase64,
                    fileName: this.project.fileName,
                    createUserId: this.user.ipdmsUserId,
                    createUserDate: this.currentDate(),
                    lastUpdateUserId: this.user.ipdmsUserId,
                    lastUpdateDate: this.currentDate(),
                    saveType: 1
                };

                console.log(data);
                console.log(JSON.stringify(data));
                this.show = true;
                FileDataService.SaveProject(JSON.stringify(data))
                    .then(response => {
                        this.busy = false;
                        this.disable = false;
                       // this.$refs.registerProjectForm.reset();//reset form
                        this.alertMessage = response.data;
                        this.error = false;
                        this.delayedAlert();
                        //this.imageResult = response.data;
                        //console.log(response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            getAgents() {
                UserDataService.GetAllUsers()
                    .then(response => {
                        var users = response.data;

                        var frmFilter = "3";

                        var filterAgents = Object.values(users).filter(function (entry) {
                            switch (frmFilter) {
                                case '3':
                                    return entry.userRoleId === 3;
                                default:
                                    return entry;
                            }
                        });

                        this.agents = filterAgents;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            },
            getApplicationTypes() {
                LookUpDataService.GetApplicationTypes()
                    .then(response => {
                        this.applicationTypes = response.data;
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            },
            currentDate() {
                const current = new Date();
                const date = `${current.getDate()}/${current.getMonth() + 1}/${current.getFullYear()}`;
                return date;
            },
            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 5000);
            },
        },
        beforeMount() {
            this.getAgents();
            this.getApplicationTypes();
            this.user = JSON.parse(sessionStorage.getItem('userInfo'));
            console.log(this.user);
            console.log(this.currentDate());
        },
        computed: {
        }
    }
</script>
<style>

    .b-overlay div{
        background-color: white !important;
    }
</style>
