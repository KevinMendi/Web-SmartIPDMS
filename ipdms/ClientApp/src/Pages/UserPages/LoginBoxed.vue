<template>
    <div>
        <form @submit.prevent="signIn">
            <div class="h-100  bg-animation" style="background-color: #38d39f;">
                <div class="d-flex h-100 justify-content-center align-items-center">
                    
                    <b-col md="8" class="mx-auto app-login-box">
                        <div class=" mx-auto mb-3" />
                        
                            <div class="modal-dialog w-100 mx-auto">
                                <div class="modal-content">
                                    <b-overlay :show="busy"
                                               rounded
                                               opacity="0.6"
                                               spinner-small
                                               spinner-variant="primary"
                                               class="d-inline-block"
                                               @hidden="onHidden">
                                        <div class="modal-body">

                                            <div class="h5 modal-title text-center">
                                                <b-alert show variant="danger" dismissible fade v-model="showAlert">
                                                    {{alertMessage}}
                                                </b-alert>
                                                <h4 class="mt-2">
                                                    <div>Smart Intellectual Property Document Management System</div>
                                                    <small>Please sign in to your account below.</small>
                                                </h4>
                                            </div>

                                            <b-form-group id="exampleInputGroup1"
                                                          label-for="exampleInput1"
                                                          description="We'll never share your email with anyone else.">
                                                <b-form-input id="exampleInput1"
                                                              type="email"
                                                              v-model="user.email"
                                                              required
                                                              placeholder="Enter email">
                                                </b-form-input>
                                            </b-form-group>
                                            <b-form-group id="exampleInputGroup2"
                                                          label-for="exampleInput2">
                                                <b-form-input id="exampleInput2"
                                                              type="password"
                                                              v-model="user.password"
                                                              required
                                                              placeholder="Enter password">
                                                </b-form-input>
                                            </b-form-group>
                                            <b-form-checkbox name="check" id="exampleCheck">
                                                Keep me logged in
                                            </b-form-checkbox>
                                            <div class="divider" />
                                            <h6 class="mb-0">
                                                No account?
                                                <router-link :to="{ name: 'register-boxed' }">Sign Up</router-link>
                                            </h6>
                                        </div>
                                    </b-overlay>
                                        <div class="modal-footer clearfix">
                                            <div class="float-right">
                                                <b-button type="submit" variant="primary" class="btn-wide btn-pill btn-shadow btn-hover-shine"
                                                          size="lg">
                                                    Sign In
                                                </b-button>

                                            </div>
                                        </div>
                                </div>
                            </div>
                        
                        <div class="text-center text-white opacity-8 mt-3">
                            Copyright &copy; IPDMS 2021
                        </div>
                    </b-col>
                       
                </div>
            </div>
         </form>
    </div>
</template>
<script>
    import UserDataService from "../../Services/UserDataService";
    export default {
        data() {
            return {
                user: {
                    email: "",
                    password: ""
                },
                userInfo: null,
                submitted: false,
                showAlert: false,
                alertMessage: "",
                busy: false
            };
        },
        beforeDestroy() {
            this.clearTimeout()
        },
        methods: {
            signIn() {
                this.busy = true;
                UserDataService.CheckCredential(this.user)
                    .then(response => {
                        this.submitted = true;
                        this.busy = false;
                        this.userInfo = response.data;
                        if (this.userInfo.ipdmsUserId != 0) {

                            this.$store.dispatch('setUserInfo', this.userInfo);
                            

                            this.$router.push({ path: 'dashboard' });
                        } else {
                            this.alertMessage = "Email or Password is incorrect!";
                            this.delayedAlert();
                        }
                     })
                     .catch(e => {
                         this.alertMessage = e;
                         this.delayedAlert();
                         this.busy = false;
                     });
            },
            delayedAlert() {
                this.showAlert = true;
                setTimeout(() => {
                    this.showAlert = false;
                }, 3000);
            },
        },
        mounted() {
        }
    }
</script>