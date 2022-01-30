<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <div class="content">
            <b-row>
                <b-col md="12">
                    <b-card class="mb-3 nav-justified" no-body>
                        <b-tabs pills card>
                            <b-tab title="Scan Document" active>
                                <form @submit.prevent="AnalyzeScanDocument" ref="ScanDocumentForm">
                                    <b-overlay :show="busy"
                                               rounded
                                               opacity="0.6"
                                               spinner-large
                                               spinner-variant="primary"
                                               class="d-inline-block col-md-12"
                                               @hidden="onHidden">
                                        <center>
                                            <div id="app" class="web-camera-container">
                                                <div class="camera-button">
                                                    <button type="button" class="button is-rounded" :class="{ 'is-primary' : !isCameraOpen, 'is-danger' : isCameraOpen}" @click="toggleCamera">
                                                        <span v-if="!isCameraOpen">Open Camera</span>
                                                        <span v-else>Close Camera</span>
                                                    </button>
                                                </div>

                                                <div v-show="isCameraOpen && isLoading" class="camera-loading">
                                                    <ul class="loader-circle">
                                                        <li></li>
                                                        <li></li>
                                                        <li></li>
                                                    </ul>
                                                </div>

                                                <div v-if="isCameraOpen" v-show="!isLoading" class="camera-box" :class="{ 'flash' : isShotPhoto }">

                                                    <div class="camera-shutter" :class="{'flash' : isShotPhoto}"></div>

                                                    <video v-show="!isPhotoTaken" ref="camera" :width="450" :height="337.5" autoplay></video>

                                                    <canvas v-show="isPhotoTaken" id="photoTaken" ref="canvas" :width="450" :height="337.5"></canvas>
                                                </div>

                                                <div v-if="isCameraOpen && !isLoading" class="camera-shoot">
                                                    <button type="button" class="button" @click="takePhoto">
                                                        <img src="https://img.icons8.com/material-outlined/50/000000/camera--v2.png">
                                                    </button>
                                                </div>

                                                <div v-if="isPhotoTaken && isCameraOpen" class="camera-download">
                                                    <!--<a id="downloadPhoto" download="my-photo.jpg" class="button" role="button" @click="downloadImage">
                    Download
                </a>-->
                                                    <button :disabled="disable" class="mt-1 btn btn-primary">Analyze Image</button>

                                                </div>
                                            </div>
                                        </center>
                                    </b-overlay>
                                </form>
                                    
                            </b-tab>
                            <b-tab title="Upload Document" active>
                                <form @submit.prevent="saveProject" ref="UploadForm">
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
                                                       class="">Document Mailing Date</label><input name="mailingDate"
                                                                                                    id="mailingDate"
                                                                                                    v-if="projectIdentifier.mailDate"
                                                                                                    placeholder="Mailing Date"
                                                                                                    v-model="projectIdentifier.mailDate"
                                                                                                    type="datetime"
                                                                                                    class="form-control" readonly required>
                                                <br />
                                                <date-picker v-if="!projectIdentifier.mailDate && !checked" v-model="projectIdentifier.mailDate" valueType="format" format="DD/MM/YYYY" required></date-picker>&nbsp;&nbsp;&nbsp;
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

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <b-button :disabled="disable" type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine"
                                                              size="lg">
                                                        Upload
                                                    </b-button>
                                                </div>
                                                <div class="col-md-6">
                                                    <b-button :disabled="disable" type="button" variant="danger" @click="refreshData()" class="btn-wide btn-pill btn-shadow btn-hover-shine float-right"
                                                              size="lg" >
                                                        Cancel
                                                    </b-button>
                                                </div>
                                            </div>
                                           

                                            
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
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import DatePicker from 'vue2-datepicker';
    import 'vue2-datepicker/index.css';

    //import 'vue-media-recorder/src/assets/scss/main.scss'
    //import { PhotoCapture } from 'vue-media-recorder'

    export default {
        components: {
            PageTitle,
            DatePicker,
           // PhotoCapture,
           // VideoCapture

        },
        data: () => ({
            heading: 'Upload Office Action Document File',
            subheading: 'Upload latest office action document of the Project via scanning the document or upload the PDF file',
            icon: 'pe-7s-upload icon-gradient bg-happy-itmeo',
            busy: false,
            disable: false,

            counter: 99,
            max: 100,
            timer: null,
            striped: true,
            image: '',
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
                fileName: "",

            },

            showAlert: false,
            alertMessage: "",
            isCameraOpen: false,
            isPhotoTaken: false,
            isShotPhoto: false,
            isLoading: false,
            link: '#',
            cavas: "",
            checked: false
        }),
        methods: {
            refreshData() {
                this.projectIdentifier = {
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
                fileName: "",
                }
            },
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
                    type: 2//Upload File
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
            AnalyzeScanDocument() {
                var data = {
                    image64: this.canvas,
                   // projectId: null,
                    type: 3,//Scan Document
                }

                //data.image64 = file;
                console.log("AnalyzeScanDocument");
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


                console.log("sullod");
                var data = {
                    applicationTypeId: this.projectIdentifier.applicationTypeId,
                    applicationNo: this.projectIdentifier.applicationNo,
                    projectTitle: "",
                    projectId: null,
                    officeActionId: this.projectIdentifier.officeActionId,
                    fillingDate: null,//add
                    mailingDate: this.projectIdentifier.mailDate,
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
                        this.error = false;
                        this.delayedAlert();
                        //this.imageResult = response.data;
                        //console.log(response.data);
                       // this.getDocumentListByProjectId(this.projectId);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });
            },
            toggleCamera() {
                if (this.isCameraOpen) {
                    this.isCameraOpen = false;
                    this.isPhotoTaken = false;
                    this.isShotPhoto = false;
                    this.stopCameraStream();
                } else {
                    this.isCameraOpen = true;
                    this.createCameraElement();
                }
            },

            createCameraElement() {
                this.isLoading = true;

                const constraints = (window.constraints = {
                    audio: false,
                    video: true
                });


                navigator.mediaDevices
                    .getUserMedia(constraints)
                    .then(stream => {
                        this.isLoading = false;
                        this.$refs.camera.srcObject = stream;
                    })
                    .catch(error => {
                        this.isLoading = false;
                        console.log(error);
                        alert("May the browser didn't support or there is some errors.");
                    });
            },

            stopCameraStream() {
                let tracks = this.$refs.camera.srcObject.getTracks();

                tracks.forEach(track => {
                    track.stop();
                });
            },

            takePhoto() {
                if (!this.isPhotoTaken) {
                    this.isShotPhoto = true;

                    const FLASH_TIMEOUT = 50;

                    setTimeout(() => {
                        this.isShotPhoto = false;
                    }, FLASH_TIMEOUT);
                }

                this.isPhotoTaken = !this.isPhotoTaken;

                const context = this.$refs.canvas.getContext('2d');
                context.drawImage(this.$refs.camera, 0, 0, 450, 337.5);

                this.canvas = document.getElementById("photoTaken").toDataURL("image/jpeg");
                //console.log(context);
                console.log(this.canvas);
            },

            downloadImage() {
                const download = document.getElementById("downloadPhoto");
                const canvas = document.getElementById("photoTaken").toDataURL("image/jpeg")
                    .replace("image/jpeg", "image/octet-stream");
                download.setAttribute("href", canvas);
            }
        
        },
        beforeMount() {
            //this.getDocumentListByProjectId(this.projectId);
            //this.getAgents();
            //this.getApplicationTypes();
            this.user = JSON.parse(sessionStorage.getItem('userInfo'));

        }


    }
</script>
<style>

    .b-overlay div {
        background-color: white !important;
    }

   /* body {
        display: flex;
        justify-content: center;
    }*/

    .web-camera-container {
        margin-top: 2rem;
        margin-bottom: 2rem;
        padding: 2rem;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        border: 1px solid #ccc;
        border-radius: 4px;
        width: 1000px;
    }

        .web-camera-container .camera-button {
            margin-bottom: 2rem;
        }

        .web-camera-container .camera-box .camera-shutter {
            opacity: 0;
            width: 450px;
            height: 337.5px;
            background-color: #fff;
            position: absolute;
        }

            .web-camera-container .camera-box .camera-shutter.flash {
                opacity: 1;
            }

        .web-camera-container .camera-shoot {
            margin: 1rem 0;
        }

            .web-camera-container .camera-shoot button {
                height: 60px;
                width: 60px;
                display: flex;
                align-items: center;
                justify-content: center;
                border-radius: 100%;
            }

                .web-camera-container .camera-shoot button img {
                    height: 35px;
                    object-fit: cover;
                }

        .web-camera-container .camera-loading {
            overflow: hidden;
            height: 100%;
            position: absolute;
            width: 100%;
            min-height: 150px;
            margin: 3rem 0 0 -1.2rem;
        }

            .web-camera-container .camera-loading ul {
                height: 100%;
                position: absolute;
                width: 100%;
                z-index: 999999;
                margin: 0;
            }

            .web-camera-container .camera-loading .loader-circle {
                display: block;
                height: 14px;
                margin: 0 auto;
                top: 50%;
                left: 100%;
                transform: translateY(-50%);
                transform: translateX(-50%);
                position: absolute;
                width: 100%;
                padding: 0;
            }

                .web-camera-container .camera-loading .loader-circle li {
                    display: block;
                    float: left;
                    width: 10px;
                    height: 10px;
                    line-height: 10px;
                    padding: 0;
                    position: relative;
                    margin: 0 0 0 4px;
                    background: #999;
                    animation: preload 1s infinite;
                    top: -50%;
                    border-radius: 100%;
                }

                    .web-camera-container .camera-loading .loader-circle li:nth-child(2) {
                        animation-delay: 0.2s;
                    }

                    .web-camera-container .camera-loading .loader-circle li:nth-child(3) {
                        animation-delay: 0.4s;
                    }

    @keyframes preload {
        0% {
            opacity: 1;
        }

        50% {
            opacity: 0.4;
        }

        100% {
            opacity: 1;
        }
    }
</style>
