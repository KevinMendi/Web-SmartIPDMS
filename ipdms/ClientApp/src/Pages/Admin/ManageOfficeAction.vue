<template>
    <div>
        <form @submit.prevent="saveOfficeAction">
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
                            <div>Add Office Action</div>
                        </h4>
                        <div class="divider" />
                        <div class="row">
                            <div class="col-md-12">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="officeActionName"
                                                  name="officeActionName"
                                                  type="text"
                                                  v-model="oa.officeActionName"
                                                  required
                                                  placeholder="Office Action *">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-12">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2">
                                    <b-form-input id="officeActionDesc"
                                                  name="officeActionDesc"
                                                  type="text"
                                                  v-model="oa.officeActionDescription"
                                                  placeholder="Office Action Description ">
                                    </b-form-input>
                                </b-form-group>
                            </div>
                            <div class="col-md-12">
                                <b-form-group id="exampleInputGroup2"
                                              label-for="exampleInput2"
                                              description='Please leave it blank if "No need for response"'>
                                    <b-form-input id="officeActionDue"
                                                  
                                                  name="officeActionDue"
                                                  type="text"
                                                  v-model="oa.officeActionDue"
                                                  placeholder="Office Action Due ">
                                    </b-form-input>
                                </b-form-group>
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
                        <b-button type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine float-right"
                                  size="lg">
                            Register Office Action
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
    import LookUpDataService from "../../Services/LookUpDataService";
    export default {
        name: "UserRegister",
        data() {
            return {
                oa: {
                    officeActionName: "",
                    officeActionDescription: "",
                    officeActionDue: null
                },
                submitted: false,
                showAlert: false,
                alertMessage: "",
                ipdmsVariant: 'success'
            };
        },
        methods: {
            saveOfficeAction() {
                var data = {
                    officeActionName1: this.oa.officeActionName,
                    officeActionName2: "",
                    officeActionName3: "",
                    officeActionDesc: this.oa.officeActionDescription,
                    officeActionDue: this.oa.officeActionDue,

                };

                LookUpDataService.SaveOfficeAction(data)
                    .then(() => {
                        this.submitted = true;
                        this.alertMessage = "Successfuly Saved!";
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
            }
        },
        mounted() {
        }
    }
</script>
