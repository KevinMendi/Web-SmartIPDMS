<template>
    <div>
        <form @submit.prevent="saveUser">
            <div class="submit-form">
                    <div class="h-100" style="background-color: #38d39f;">
                        <div class="d-flex h-100 justify-content-center align-items-center">
                            <b-col md="8" class="mx-auto app-login-box">
                                <div class=" mx-auto mb-3" />
                                
                                <div class="modal-dialog w-100">
                                    <div class="modal-content">
                                        <div class="modal-body">
                                            <b-alert show variant="danger" dismissible fade v-model="showAlert">
                                                {{alertMessage}}
                                            </b-alert>
                                            <h4 class="mt-2">
                                                <div>Welcome to IPDMS,</div>
                                                <span>It only takes a <span class="text-success">few seconds</span> to create your account</span>
                                            </h4>
                                            <div class="divider" />
                                            <select class="mb-2 form-control" name="userRoleId" id="userRoleId" v-model="user.userRoleId">
                                                <option value="-1" disabled>Please select *</option>
                                                <option value="1">Super Admin</option>
                                                <option value="2">Admin</option>
                                                <option value="3">Agent</option>
                                            </select>
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <b-form-group id="exampleInputGroup2"
                                                                  label-for="exampleInput2">
                                                        <b-form-input id="firstname"
                                                                      name="firstname"
                                                                      type="text"
                                                                      v-model="user.firstname"
                                                                      required
                                                                      placeholder="First Name *">
                                                        </b-form-input>
                                                    </b-form-group>
                                                </div>
                                                <div class="col-md-5">
                                                    <b-form-group id="exampleInputGroup2"
                                                                  label-for="exampleInput2">
                                                        <b-form-input id="lastname"
                                                                      name="lastname"
                                                                      type="text"
                                                                      v-model="user.lastname"
                                                                      required
                                                                      placeholder="Last Name *">
                                                        </b-form-input>
                                                    </b-form-group>
                                                </div>
                                                <div class="col-md-2">
                                                    <b-form-group id="exampleInputGroup2"
                                                                  label-for="exampleInput2">
                                                        <b-form-input id="middlename"
                                                                      name="middlename"
                                                                      type="text"
                                                                      v-model="user.middlename"
                                                                      placeholder="MI.">
                                                        </b-form-input>
                                                    </b-form-group>
                                                </div>
                                            </div>
                                            <b-form-group id="exampleInputGroup1"
                                                          label-for="exampleInput1"
                                                          description="We'll never share your email with anyone else.">
                                                <b-form-input id="email"
                                                              name="email"
                                                              type="email"
                                                              v-model="user.email"
                                                              required
                                                              placeholder="Enter email *">
                                                </b-form-input>
                                            </b-form-group>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <b-form-group id="exampleInputGroup2"
                                                                  label-for="exampleInput2">
                                                        <b-form-input id="password"
                                                                      name="password"
                                                                      type="password"
                                                                      v-model="user.password"
                                                                      required
                                                                      placeholder="Enter password *">
                                                        </b-form-input>
                                                    </b-form-group>
                                                </div>
                                                <div class="col-md-6">
                                                    <b-form-group id="exampleInputGroup2"
                                                                  label-for="exampleInput2">
                                                        <b-form-input id="password2"
                                                                      name="password2"
                                                                      type="password"
                                                                      v-model="user.password2"
                                                                      required
                                                                      placeholder="Repeat password *">
                                                        </b-form-input>
                                                    </b-form-group>
                                                </div>
                                            </div>
                                            <b-form-checkbox name="check" id="exampleCheck">
                                                Accept our <a href="javascript:void(0);">Terms and Conditions</a>.
                                            </b-form-checkbox>
                                            <div class="divider" />
                                            <h6 class="mb-0">
                                                Already have an account?
                                                <router-link :to="{ name: 'login-boxed' }" class="text-primary">Sign in</router-link>
                                            </h6>
                                        </div>
                                        <div class="modal-footer d-block text-center">
                                            <!--<b-button @click="saveUser" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine"
                          size="lg">
                    Create Account
                </b-button>-->
                                            <b-button type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine"
                                                      size="lg">
                                                Create Account
                                            </b-button>
                                        </div>
                                    </div>
                                </div>
                                <div class="text-center text-white opacity-8 mt-3">
                                    Copyright &copy; IPDMS 2021
                                </div>
                            </b-col>
                        </div>
                    </div>
            </div>
         </form>
    </div>
</template>

<script>
    /*import axios from 'axios'*/
    import UserDataService from "../../Services/UserDataService";
    export default {
        name: "RegisterUser",
        data() {
            return {
                user: {
                    firstname: null,
                    lastname: "",
                    middlename: "",
                    userRoleId: -1,
                    email: "",
                    password: ""
                },
                submitted: false,
                showAlert: false,
                alertMessage: ""
            };
        },
        methods: {
            saveUser() {
                var data = {
                    firstname: this.user.firstname,
                    middlename: this.user.middlename,
                    lastname: this.user.lastname,
                    userRoleId: parseInt(this.user.userRoleId),
                    email: (this.user.email).trim(),
                    password: (this.user.password).trim(),
                    password2: (this.user.password2).trim()

                };

                if ((data.password).length < 8) {
                    this.alertMessage = "Password should be greater than 6 characters!";
                    this.delayedAlert();
                }
                else if (data.password != data.password2) {
                    this.alertMessage = "Passwords do not match!";
                    this.delayedAlert();
                }
                else {
                    UserDataService.SaveUser(data)
                        .then(response => {
                            this.user.user_id = response.data.id;
                            this.submitted = true;
                        })
                        .catch(e => {
                            alert(e);
                        });
                }
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
