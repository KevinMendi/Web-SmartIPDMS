<template>
    <div>
        <form @submit.prevent="saveProject">
            <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
            <div class="submit-form">
                <div class="content">
                    <div class="main-card mb-3 card">
                        <div class="card-body">
                            <!--<h5 class="card-title">Grid Rows</h5>-->
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <div class="position-relative form-group">
                                            <label for="applicationType" class="">Application Type</label>
                                            <select name="applicationType" id="applicationType" class="form-control" v-model="project.applicationType" required>
                                                <option value="1">Invention</option>
                                                <option value="2">Utility Model</option>
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
                                <div class="form-row">
                                    <div class="col-md-3">
                                        <div class="position-relative form-group">
                                            <label for="filingDate"
                                                   class="">Document Filing Date</label>
                                            <date-picker v-model="project.fillingDate" valueType="format" required></date-picker>

                                        </div>
                                    </div>
                                    <div class="col-md-5">
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
                                    <div class="col-md-4">
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
                                                <option v-for="(agent, index) in agents" :key="index">{{ agent.firstname + ' ' + agent.lastname  }}</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="position-relative form-group">
                                    <label for="exampleFile"
                                           class="">File</label><input name="file"
                                                                       id="exampleFile"
                                                                       type="file"
                                                                       @change="onFileChange"
                                                                       class="form-control-file"
                                                                       required>
                                    <small class="form-text text-muted">
                                        Please upload "Acknowledgement Office Action".
                                    </small>
                                </div>
                                <button class="mt-2 btn btn-primary">Register Project</button>
                        </div>
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
            icon: 'pe-7s-upload icon-gradient bg-happy-itmeo',

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
                applicantName: "",
                agentName: "",
                pdfBase64: ""
            },
            agents: [],
            stringJSON: '',
            submitted: false,
            showAlert: false,
            alertMessage: ""

        }),
        methods: {
           
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
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
                var data = {
                    applicationTypeId: this.project.applicationType,
                    applicationNo: (this.project.applicationNumber).trim(),
                    projectTitle: (this.project.projectName).trim(),
                    officeActionId: this.project.officeAction,
                    fillingDate: this.project.fillingDate,//add
                    applicantName: (this.project.applicantName).trim(),
                    agentName: this.project.agentName,//add
                    pdfBase64: this.project.pdfBase64
                };

               console.log(data);

                FileDataService.SaveProject(JSON.stringify(data))
                    .then(response => {
                        console.log(response);
                        //this.imageResult = response.data;
                        //console.log(response.data);
                    })
                    .catch(e => {
                        this.imageResult = e;
                    });
            },
            getAgents() {
                console.log("filterAgents");
                UserDataService.GetAllUsers()
                    .then(response => {
                        var users = response.data;

                        var frmFilter = "3";

                        //var filterAgents = Object.keys(users).filter(function (key) {
                        //    let entry = users[key];
                        //    switch (frmFilter) {
                        //        case '3':
                        //            return entry.userRoleId === 3;
                        //        default:
                        //            return entry;
                        //    }
                        //}).reduce((res, key) => (res[key] = users[key], res), {});

                        var filterAgents = Object.values(users).filter(function (entry) {
                            switch (frmFilter) {
                                case '3':
                                    return entry.userRoleId === 3;
                                default:
                                    return entry;
                            }
                        });

                        this.agents = filterAgents;
                        //console.log("filterAgents");
                        //console.log(filterAgents);
                        //this.submitted = true;
                        console.log("sulod");
                        console.log(users);
                        console.log(this.agents);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            }

        },
        beforeMount() {
            this.getAgents()
        },
        computed: {
        }


    }
</script>
