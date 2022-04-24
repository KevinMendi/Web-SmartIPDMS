<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <b-alert show dismissible fade variant="info"> There is no uploaded Office Action yet for this Project! </b-alert>
        <div class="main-card mb-3 card">
            <div class="card-body">
                <div class="row">
                    <div class="col-sm-12 col-md-6 col-xl-6">

                        <b>Agent:</b>&nbsp;&nbsp; {{ user.firstname + ' ' + user.lastname  }}<br />
                        <b>Applicant Name:</b>&nbsp;&nbsp; {{ newlyConvertedUM.applicantName }}<br />
                        <b> Application Type/No:</b>&nbsp;&nbsp; {{ newlyConvertedUM.applicationTypeId == 1 ? "Invention" : "Utility Model" + ' ' + newlyConvertedUM.applicationNo}}<br />
                        <b>Title:</b>&nbsp;&nbsp; {{ newlyConvertedUM.projectTitle }} <br />
                    </div>

                    <div class="col-sm-12 col-md-6 col-xl-6 d-inline-flex p-2 d-flex justify-content-end">
                        <!--<b-button class="mr-2 mb-2 border-0 btn-transition" variant="info"> + Upload </b-button>-->
                        <b-button v-b-modal.modal-multi-1 class="mr-2 mb-2 border-0 btn-transition" variant="info">+ Upload</b-button>

                        <b-modal id="modal-multi-1" size="lg" title="Project Detail" ok-only ok-variant="secondary" ok-title="Cancel" no-stacking @ok="refreshData()">
                            <div class="content">
                                <b-row>
                                    <b-col md="12">
                                        <b-card class="mb-3 nav-justified" no-body>
                                            <b-tabs pills card>
                                         
                                                <b-tab title="Upload Document" active>
                                                    <form @submit.prevent="saveProject" ref="ProjectDetailForm">
                                                        <b-overlay :show="busy"
                                                                   rounded
                                                                   opacity="0.6"
                                                                   spinner-large
                                                                   spinner-variant="primary"
                                                                   class="d-inline-block col-md-12"
                                                                   @hidden="onHidden">
                                                            <div class="card-body">
                                                                <div class="position-relative form-group">
                                                                    <label for="exampleFile"
                                                                           class="">File</label><input name="file"
                                                                                                       id="uploadFile"
                                                                                                       type="file"
                                                                                                       @change="onFileChange"
                                                                                                       class="form-control-file">
                                                                    <small class="form-text text-muted">
                                                                        After selecting file to be uploaded, click button "Get Details".
                                                                    </small>
                                                                    <!--{{imageResult}}-->
                                                                </div>
                                                                <!--<div class="">
                                                    <b-button v-b-toggle.collapseDetails block class="mr-2 mb-2" variant="info" :size="sm" :key="sm">
                                                        Check Details
                                                    </b-button>
                                                </div>-->
                                                                <!-- elements to collapse -->
                                                                <!--<b-collapse id="collapseDetails" class="mt-2">-->

                                                                <div class="position-relative form-group">
                                                                    <label for="applicationType" class="">Application Type</label>
                                                                    <!--<select name="applicationType" id="applicationType" class="form-control" v-model="projectIdentifier.applicationType" required>
                                                        <option :value="projectIdentifier.applicationType"> projectIdentifier.applicationType</option>
                                                        <option v-for="(applicationType, index) in applicationTypes" :key="index" :value="applicationType.applicationTypeId">{{ applicationType.applicationTypeName }}</option>
                                                    </select>-->
                                                                    <input name="applicationType"
                                                                           v-if="projectIdentifier.applicationTypeName"
                                                                           id="applicationType"
                                                                           placeholder="Application Type"
                                                                           v-model="projectIdentifier.applicationTypeName"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="applicationType"
                                                                           v-if="!projectIdentifier.applicationTypeName"
                                                                           id="applicationType"
                                                                           placeholder="Application Type"
                                                                           v-model="projectIdentifier.applicationTypeName"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>
                                                                <div class="position-relative form-group">
                                                                    <label for="applicationNumber"
                                                                           class="">Application Number</label>
                                                                    <input name="applicationNumber"
                                                                           v-if="projectIdentifier.applicationNo"
                                                                           id="applicationNumber"
                                                                           placeholder="Application Number"
                                                                           v-model="projectIdentifier.applicationNo"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="applicationNumber"
                                                                           v-if="!projectIdentifier.applicationNo"
                                                                           id="applicationNumber"
                                                                           placeholder="Application Number"
                                                                           v-model="projectIdentifier.applicationNo"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>


                                                                <div class="position-relative form-group">
                                                                    <label for="officeAction" class="">Office Action:</label>
                                                                    <!--<select name="officeAction"
                                                                                                                     id="officeAction"
                                                                                                                     class="form-control">
                                                        <option value="1">Acknowledgement</option>
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

                                                    </select>-->
                                                                    <input name="officeAction"
                                                                           v-if="projectIdentifier.officeActionName"
                                                                           id="officeAction"
                                                                           placeholder="Office Action"
                                                                           v-model="projectIdentifier.officeActionName"
                                                                           type="text"
                                                                           class="form-control" readonly required>

                                                                    <input name="officeAction"
                                                                           v-if="!projectIdentifier.officeActionName"
                                                                           id="officeAction"
                                                                           placeholder="Office Action"
                                                                           v-model="projectIdentifier.officeActionName"
                                                                           type="text"
                                                                           class="form-control" required>
                                                                </div>
                                                                <div class="position-relative form-group">
                                                                    <label for="mailingDate"
                                                                           class="">Document Mailing Date</label>

                                                                    <input name="mailingDate"
                                                                           v-if="projectIdentifier.mailDate && projectIdentifier.mailDate.length == 10 && !checked"
                                                                           id="mailingDate"
                                                                           placeholder="Mailing Date"
                                                                           v-model="projectIdentifier.mailDate"
                                                                           type="datetime"
                                                                           class="form-control" readonly required>
                                                                    <br />
                                                                     <date-picker v-if="(!projectIdentifier.mailDate && !checked) || (projectIdentifier.mailDate.length < 10 && !checked)" v-model="projectIdentifier.mailDate" valueType="format" format="DD/MM/YYYY" required></date-picker>&nbsp;&nbsp;&nbsp;
                                                                    <span><input type="checkbox" id="checkbox" v-model="checked"> Please check if mailing date is not applicable.</span>
                                                                </div>
                                                                <!--<div class="position-relative form-group">
                                                    <label for="agentName"
                                                           class="">Agent Name</label><input name="agentName"
                                                                                             id="agentName"
                                                                                             placeholder="Agent Name"
                                                                                             type="text"
                                                                                             class="form-control">

                                                </div>-->
                                                                <b-alert :class="error ? 'danger' : 'success'" show dismissible fade v-model="showAlert">
                                                                    {{alertMessage}}
                                                                </b-alert><br />
                                                                          <b-button :disabled="!projectIdentifier.mailDate && !checked ? true : false" type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine"
                                                                                    size="lg">
                                                                              Save
                                                                          </b-button>
                                                                <!--</b-overlay>-->
                                                                <!--</b-collapse>-->
                                                            </div>
                                                        </b-overlay>
                                                    </form>
                                                </b-tab>
                                            </b-tabs>
                                        </b-card>
                                    </b-col>
                                </b-row>
                            </div>
                        </b-modal>
                    </div>

                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import LookUpDataService from "../../Services/LookUpDataService";
    import UserDataService from "../../Services/UserDataService";
    //import DatePicker from 'vue2-datepicker';
    import 'vue2-datepicker/index.css';

    export default {
        name: 'converted-project-detail',
        components: {
           PageTitle,
            //DatePicker,

        },
        props: {
            projectId: {
                type: String,
                required: true
            }
        },
        data() {
            return {
                heading: 'Smart Intellectual Property Document Management System',
                subheading: 'Short project description here ',
                icon: 'pe-7s-portfolio icon-gradient bg-happy-itmeo',
                busy: false,
                disable: false,

                counter: 99,
                max: 100,
                timer: null,
                striped: true,
                items: [
                    /*         { isActive: false, office_action: { icon: 'pe-7s-file', type: 'Acknowledgement' }, file: { fname: '2017_NOWA_PA12014000317.pdf' }, file_size: '2kb' },*/

                ],
                fields: [
                    { key: 'officeAction', label: 'Office Action/Document', sortable: true, sortDirection: 'desc' },
                    { key: 'file', label: 'File Name', sortable: true, class: 'text-center' },
                    { key: 'fileSize', label: 'File size', sortable: true, class: 'text-center' },
                    //{ key: 'agent_name', label: 'Agent', sortable: true, sortDirection: 'desc' },
                    //{
                    //    /*key: 'isActive',*/
                    //    label: 'Is Active',
                    //    formatter: (value) => {
                    //        return value ? 'Yes' : 'No'
                    //    },
                    //    sortable: true,
                    //    sortByFormatted: true,
                    //    filterByFormatted: true
                    //},
                    { key: 'actions', label: 'Actions', class: 'text-end' }
                ],
                totalRows: 1,
                currentPage: 1,
                perPage: 5,
                pageOptions: [5, 10, 15, { value: 100, text: "Show a lot" }],
                sortBy: '',
                sortDesc: false,
                sortDirection: 'asc',
                filter: null,
                filterOn: [],
                infoModal: {
                    id: 'info-modal',
                    title: '',
                    content: ''
                },
                pdfBase64: "",
                deleteDocId: 0,
                setDocId: 0,
                image: "",
                imageResult: '',
                projectIdentifier: {
                    applicationTypeId: null,
                    applicationTypeName: "",
                    applicationNo: "",
                    officeActionId: null,
                    officeActionName: "",
                    mailDate: "",
                    projectTitle: "",
                    fillingDate: null,
                    applicantName: "",
                    agentName: "",
                    fileName:"",

                },
                showAlert: false,
                alertMessage: "",
                agents: [],
                applicationTypes: [],
                user: null,
                setRDate: null,
                checked: false,
                finishProjectId: 0,
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
                newApplicationNumber: null,
                convertedItems: [],
                newlyConvertedUM: null
            }
        },

        methods: {
            refreshData() {
                this.projectIdentifier =  {
                    applicationTypeId: null,
                    applicationTypeName: "",
                    applicationNo: "",
                    officeActionId: null,
                    officeActionName: "",
                    mailDate: "",
                    projectTitle: "",
                    fillingDate: null,
                    applicantName: "",
                    agentName: "",
                    fileName:"",

                }
            },
            onShown() {
                // Focus the cancel button when the overlay is showing
                this.$refs.cancel.focus()
            },
            onHidden() {
                // Focus the show button when the overlay is removed
                this.$refs.show.focus()
            },
            hideModalConvert() {
                this.$refs['utility-model'].hide()
            },
            toggleModalConvert() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['utility-model'].toggle('#toggle-btn')
            },
            toggleModalConvertUM() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['utility-model-convert'].toggle('#toggle-btn')
            },

            hideModalFinish() {
                this.$refs['finish-project'].hide()
            },
            toggleModalFinish() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['finish-project'].toggle('#toggle-btn')
            },
            toggleModalFinishProject(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.finishProjectId = id;
                this.$refs['finish-project'].toggle('#toggle-btn')
            },
            hideModalDelete() {
                this.$refs['delete-document'].hide()
            },
            hideModalSetResponseDate() {
                this.$refs['set-response-date'].hide()
            },
            toggleModalDelete() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['delete-document'].toggle('#toggle-btn')
            },
            toggleModalDeleteDocumentById(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.deleteDocId = id;
                this.$refs['delete-document'].toggle('#toggle-btn')
            },
            toggleSetResponseDate(id) {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.setDocId = id;
                this.$refs['set-response-date'].toggle('#toggle-btn')
            },
            toggleModalSetResponseDate() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['set-response-date'].toggle('#toggle-btn')
            },
            info(item, index /*, button*/) {
                this.infoModal.title = `Row index: ${index}`
                this.infoModal.content = JSON.stringify(item, null, 2)
                //   this.$root.$emit('bv::show::modal', this.infoModal.id, button)
            },
            resetInfoModal() {
                this.infoModal.title = ''
                this.infoModal.content = ''
            },
            onFiltered(filteredItems) {
                // Trigger pagination to update the number of buttons/pages due to filtering
                this.totalRows = filteredItems.length
                this.currentPage = 1
            },
            getDocumentListByProjectId(id) {
                FileDataService.GetDocumentListByProjectId(id)
                    .then(response => {

                        this.items = response.data;
                        console.log("getDocumentListByProjectId");
                        console.log(this.items);
                        this.redirect();
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            downloadPdfFile(folder, fname) {
                //LookUpDataService.GetDocumentById(id)
                //    .then(response => {
                //        this.pdfBase64 = response.data.pdfContent;

                //        const downloadLink = document.createElement("a");
                //        const fileName = response.data.pdfName;

                //        downloadLink.href = this.pdfBase64;
                //        downloadLink.download = fileName;
                //        downloadLink.click();
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;
                //    });

                var link = document.createElement('a');
                link.href = `${folder}/${fname}`;
                link.download = fname;
                link.dispatchEvent(new MouseEvent('click'));
            },
            previewPdfFile(folder, fname) {
                //LookUpDataService.GetDocumentById(id)
                //    .then(response => {
                //        this.pdfBase64 = response.data.pdfContent;

                //        var byteCharacters = atob(this.pdfBase64.substring(28));
                //        var byteNumbers = new Array(byteCharacters.length);
                //        for (var i = 0; i < byteCharacters.length; i++) {
                //            byteNumbers[i] = byteCharacters.charCodeAt(i);
                //        }
                //        var byteArray = new Uint8Array(byteNumbers);
                //        var file = new Blob([byteArray], { type: 'application/pdf;base64' });
                //        var fileURL = URL.createObjectURL(file);
                //        window.open(fileURL);
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;
                //    });
                window.open(`${folder}/${fname}`, '_blank');
            },
            deletePdfFile(id) {

                LookUpDataService.GetDocumentById(id)
                    .then(response => {
                        response.data.isDeleted = true;
                        console.log("deletePdfFile");
                        console.log(response.data);
                        this.flagDocumentDeleted(id, response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });


            },
            flagDocumentDeleted(id, data) {

                LookUpDataService.DeleteDocumentById(id, data)
                    .then(result => {
                        console.log(result.data);
                        this.hideModalDelete();
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                    });
            },
            //Upload Document
            onFileChange(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;

                var fileInput = document.getElementById('uploadFile');
                this.projectIdentifier.fileName = fileInput.files[0].name;
                this.createImage(files[0]);
            },
            createImage(file) {
                var data = {
                    image64: this.image,
                    projectId: null,
                    type: 1//Upload File - FileManager
                }
                var reader = new FileReader();
                //var vm = this;

                reader.onload = (e) => {
                    this.image = e.target.result;
                    data.image64 = e.target.result;
                    data.projectId = this.projectId;
                    console.log(data);
                    this.busy = true;
                    this.disable = true;
                    FileDataService.AnalyzeImage(data)
                        .then(response => {
                            console.log(response);
                            this.busy = false;
                            this.disable = false;
                            this.imageResult = response.data;
                            this.projectIdentifier.applicationTypeId = response.data.applicationTypeId;
                            this.projectIdentifier.applicationTypeName = response.data.applicationTypeName;
                            this.projectIdentifier.applicationNo = response.data.applicationNo;
                            this.projectIdentifier.officeActionId = response.data.officeActionId;
                            this.projectIdentifier.officeActionName = response.data.officeActionName;
                            this.projectIdentifier.mailDate = response.data.mailDate;

                        })
                        .catch(e => {
                            this.alertMessage = e;
                            this.error = true;
                            this.busy = false;
                            this.disable = false;
                        });
                };
                reader.readAsDataURL(file);

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
                        console.log("applicationTypes");
                        console.log(this.applicationTypes);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            },
            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 5000);
            },
            currentDate() {
                const current = new Date();
                const date = `${current.getDate()}/${current.getMonth() + 1}/${current.getFullYear()}`;
                return date;
            },
            saveProject() {
                this.busy = true;
                this.disable = true;



                var data = {
                    applicationTypeId: this.projectIdentifier.applicationTypeId,
                    applicationNo: this.projectIdentifier.applicationNo,
                    projectTitle: "",
                    projectId: this.projectId,
                    officeActionId: this.projectIdentifier.officeActionId,
                    fillingDate: null,//add
                    mailingDate: this.checked ? null : this.projectIdentifier.mailDate,
                    applicantName: "",
                    agentName: "",//add
                    pdfBase64: this.image,
                    fileName: this.projectIdentifier.fileName,
                    createUserId: this.user.ipdmsUserId,
                    createUserDate: this.currentDate(),
                    lastUpdateUserId: this.user.ipdmsUserId,
                    lastUpdateDate: this.currentDate(),
                    saveType: 2
                };

                //var data2 = {
                //    pdf: this.pdfData
                //};

                console.log(data);
                this.show = true;
                FileDataService.SaveProject(JSON.stringify(data))
                    .then(response => {
                        this.busy = false;
                        this.disable = false;
                        // this.$refs.registerProjectForm.reset();//reset form
                        this.alertMessage = response.data;

                        //this.$router.push({ name: 'dashboard' });
                        //console.log("this.$router.resolve({ name: 'project-detail', query: { projectId: 5044 } })");

                        this.error = false;
                        this.delayedAlert();

                        //this.imageResult = response.data;
                        //console.log(response.data);
                        //this.getDocumentListByProjectId(this.projectId);
                        
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            setResponseDate(id) {
                console.log("setResponseDate");
                console.log(id);
                //var data = {
                //    responseDate: this.setRDate
                //};
                //console.log(data);
                //this.flagResponseDate(id, data);
                LookUpDataService.GetDocumentById(id)
                    .then(response => {
                        var dateParts = this.setRDate.split("/");
                        // month is 0-based, that's why we need dataParts[1] - 1
                        var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0] + 1);

                        response.data.responseDate = dateObject;
                        console.log("deletePdfFile");
                        console.log(response.data);
                        this.flagResponseDate(id, response.data);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;

                    });

                //FileDataService.SetResponseDate()
                //    .then(response => {
                //        response.data.responseDate = this.setRDate;
                //        console.log("deletePdfFile");
                //        console.log(response.data);
                //        this.flagResponseDate(id, response.data);
                //    })
                //    .catch(e => {
                //        this.alertMessage = e;
                //        this.error = true;

                //    });


            },
            flagResponseDate(id, data) {
                console.log("flagResponseDate");
                console.log(id);
                console.log(data);
                LookUpDataService.DeleteDocumentById(id, data)
                    .then(result => {
                        console.log("flagResponseDate");
                        console.log(result.data);
                        this.hideModalSetResponseDate();
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        console.log(e);
                        this.error = true;
                    });
            },
            updateProject(id) {

                var data = {
                    projectId: id,
                    applicationTypeId: this.project.appTypeId,
                    projectStatusId: 1,
                    ipdmsUserId: this.project.agentId,
                    applicationNo: this.project.appNumber.trim(),
                    projectTitle: this.project.projectTitle.trim(),
                    applicantName: this.project.applicantName.trim(),
                    createUserId: this.project.createUserId,
                    lastUpdateUserId: this.project.lastUpdateUserId,
                    isDeleted: false
                };

                FileDataService.UpdateProject(id, data)
                    .then(response => {
                        console.log(response.data);
                        this.getDocumentListByProjectId(id);
                        this.$refs['finish-project'].toggle('#toggle-btn')
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });
            },
            finishProject(id) {
                console.log("finishProject");
                console.log(id);
                FileDataService.GetProjectById(id)
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

                        this.updateProject(id);

                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.delayedAlert();
                    });

            },
            convertToUtilityModel() {
                this.busy = true;
                this.disable = true;

                var data = {
                    applicationTypeId: 2,// Utility Model
                    applicationNo: this.newApplicationNumber,
                    projectTitle: this.items[0].project.projectTitle,
                   /* officeActionId: this.project.officeAction,*/
                    //fillingDate: this.project.fillingDate,//add
                    //mailingDate: this.project.mailingDate,
                    applicantName: this.items[0].project.applicantName,
                    agentName: this.user.ipdmsUserId,
                  /*  pdfBase64: this.project.pdfBase64,*/
                  /*  fileName: this.project.fileName,*/
                    createUserId: this.user.ipdmsUserId,
                    createUserDate: this.currentDate(),
                    lastUpdateUserId: this.user.ipdmsUserId,
                    lastUpdateDate: this.currentDate(),
                    saveType: 3,// Convert to Utility Model
                    refProjectId: this.items[0].project.projectId

                };

                console.log("convertToUtilityModel");
                console.log(data);

                this.show = true;
                FileDataService.SaveProject(JSON.stringify(data))
                    .then(response => {
                        this.busy = false;
                        this.disable = false;
                        // this.$refs.registerProjectForm.reset();//reset form
                        this.alertMessage = response.data;
                        this.error = false;
                        this.delayedAlert();
                        this.toggleModalConvertUM(); this.toggleModalConvert();
                        this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            getConvertedProjectDetailsById(id) {
                FileDataService.GetConvertedProjectDetailsById(id)
                    .then(response => {
                        this.busy = false;
                        this.disable = false;

                        this.convertedItems = response.data;
                        console.log("getConvertedProjectDetailsById");
                        console.log(this.convertedItems);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            getProjectById(id) {
                LookUpDataService.GetProjectById(id)
                    .then(response => {
                        console.log("getProjectById");
                        this.newlyConvertedUM = response.data;

                        console.log("newlyConvertedUM");
                        console.log(this.newlyConvertedUM );



                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            redirect() {
                if (this.items.length > 0) {
                    this.$router.push({ name: 'project-detail', query: { projectId: 5044 } })
                   
                }
            }
        },
        beforeMount() {
            this.getDocumentListByProjectId(this.projectId);
            this.getConvertedProjectDetailsById(this.projectId);
            this.getProjectById(this.projectId);
            this.getAgents();
            this.getApplicationTypes();
            this.user = JSON.parse(sessionStorage.getItem('userInfo'));
        }
    }
</script>
<style>

    .b-overlay div {
        background-color: white !important;
    }
</style>