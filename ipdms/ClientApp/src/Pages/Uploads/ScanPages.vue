<template>
    <div>
        <page-title :heading=heading :subheading=subheading :icon=icon></page-title>
        <div class="content">
            <b-row>
                <b-col md="12">
                    <b-card class="mb-3 nav-justified" no-body>
                        
             

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
                                        </div><br />

                                        <!--<div v-if="isCameraOpen && !isLoading" class="position-relative form-group">
        <label for="applicationNumber"
               class="">Number of Pages</label>

        <input name="numberOfPages"
               id="numberOfPages"
               placeholder="Number of Pages"
               v-model="pages"
               type="number"
               min="1"
               class="form-control" required>
    </div>-->

                                        <div v-if="isCameraOpen && !isLoading" class="position-relative form-group">
                                            <label for="applicationNumber"
                                                   class="">Number of Pages Left</label>

                                            <input name="numberOfPagesLeft"
                                                   id="numberOfPagesLeft"
                                                   placeholder="Number of Pages"
                                                   v-model="numberOfPagesLeft"
                                                   type="number"
                                                   min="1"
                                                   class="form-control" readonly>
                                        </div>

                                        <div v-if="isCameraOpen && !isLoading && numberOfPagesLeft > 0" class="camera-shoot">
                                            <button type="button" class="button" @click="takePhoto">
                                                <img src="https://img.icons8.com/material-outlined/50/000000/camera--v2.png">
                                            </button>
                                        </div>

                                        <div v-if="isPhotoTaken && isCameraOpen" class="camera-download">
                                            <button v-if="numberOfPagesLeft > 0" :disabled="disable" @click="SavePages" class="mt-1 btn btn-primary"> Save Page {{ pageCount }}</button>

                                            <!--<pre>{{ canvas }}</pre>-->

                                        </div>
                                        <button v-if="numberOfPagesLeft == 0" :disabled="disable" @click="SavePages(); redirect();" class="mt-1 btn btn-primary">Finish</button>
                                        <br /><br />
                                        <b-alert :class="error ? 'danger' : 'success'" show dismissible fade v-model="showAlert">
                                            {{alertMessage}}
                                        </b-alert><br />
                                    </div>
                                </center>
                            </b-overlay>

                            <b-modal ref="scan-info" hide-header hide-footer title="Info" size="md">
                                <div class="page-title-icon d-flex justify-content-center">
                                    <i class="pe-7s-camera icon-gradient bg-happy-green fa-10x" />
                                </div>
                                <div class="d-block text-center">
                                    <div class="d-block text-left">
                                        <b>NOTE: </b><br />
                                        Do not reload the page and make sure to finish all the pages. Otherwise, you have to delete the document/office action and scan the document again.
                                    </div>
                                </div>

                                <b-button class="mt-3" :disabled="false" block @click="toggleModalScanInfo(); toggleCamera();" variant="outline-success" key="success">Proceed</b-button>


                            </b-modal>
                        
                    </b-card>
                </b-col>
            </b-row>
        </div>
    </div>
</template>

<script>
    import PageTitle from "../../Layout/Components/PageTitle.vue";
    import FileDataService from "../../Services/FileDataService";
    import LookUpDataService from "../../Services/LookUpDataService";
    //import DatePicker from 'vue2-datepicker';
    import 'vue2-datepicker/index.css';

    //import 'vue-media-recorder/src/assets/scss/main.scss'
    //import { PhotoCapture } from 'vue-media-recorder'

    export default {
        name: 'scan-pages',
        components: {
            PageTitle,
            //DatePicker,
           // PhotoCapture,
           // VideoCapture

        },
        props: {
            documentId: {
                type: String,
                required: true
            },
            pages: {
                type: Number,
                required: true
            }
        },
        data: () => ({
            heading: 'Take A Photo',
            subheading: 'Make sure to take a photo each pages',
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
            checked: false,
            applicationTypes: [],
            officeActions: [],
            pdfPages: 0,
            isAnalyzed: false,
            saveType: 2,
            numberOfPagesLeft: 0,
            pageCount: 1//this.pages
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
            hideModalScanInfo() {
                this.$refs['scan-info'].hide()
            },
            toggleModalScanInfo() {
                // We pass the ID of the button that we want to return focus to
                // when the modal has hidden
                this.$refs['scan-info'].toggle('#toggle-btn')
            },
            SavePages() {
                this.pageCount += 1;
                if (this.numberOfPagesLeft != 0) {
                    this.numberOfPagesLeft -= 1;
                }

                var data = {
                    image64: this.canvas,
                    documentId: this.documentId,
                    type: 3,//Scan Document
                    currentPage: this.pages - this.numberOfPagesLeft
                }

               
                
                FileDataService.SavePages(data)
                    .then(response => {
                        console.log(response);
                        this.busy = false;
                        this.disable = false;
                        this.alertMessage = response.data;
                        this.error = false;
                        this.delayedAlert();
                        this.takePhoto();
                    })
                    .catch(e => {
                        this.alertMessage = e;
                        this.error = true;
                        this.busy = false;
                        this.disable = false;
                    });

                
                console.log(data);

            },
            redirect() {
                this.$router.push({ name: 'file-manager'})
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

                    //if (this.projectIdentifier.mailDate == "" && !this.checked) {
                    //    this.disable = true;
                    //}
                };
                reader.readAsDataURL(file);

            },
            AnalyzeScanDocument() {
                var data = {
                    image64: this.canvas,
                   // projectId: null,
                    type: 3,//Scan Document
                }
                this.saveType = 3;
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
                    mailingDate: this.checked ? null : this.projectIdentifier.mailDate,
                    applicantName: "",
                    agentName: "",//add
                    pdfBase64: this.saveType == 2 ? this.image : this.canvas,
                    fileName: this.saveType == 2 ? this.projectIdentifier.fileName : this.projectIdentifier.officeActionName,
                    createUserId: this.user.ipdmsUserId,
                    createUserDate: this.currentDate(),
                    lastUpdateUserId: this.user.ipdmsUserId,
                    lastUpdateDate: this.currentDate(),
                    saveType: this.saveType
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
            getOfficeActions() {
                LookUpDataService.GetOfficeActions()
                    .then(response => {
                        this.officeActions = response.data;
                        console.log(this.officeActions);
                    })
                    .catch(e => {
                        this.alertMessage = e;
                    });
            }


        },
        mounted() {
            this.toggleModalScanInfo();
        },
        beforeMount() {
            //this.getDocumentListByProjectId(this.projectId);
            //this.getAgents();
            //this.getApplicationTypes();
            
            this.numberOfPagesLeft = this.pages
            this.user = JSON.parse(sessionStorage.getItem('userInfo'));
            this.getApplicationTypes();
            this.getOfficeActions();
            

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
