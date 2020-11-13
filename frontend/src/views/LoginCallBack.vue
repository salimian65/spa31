<template>
  <h3>Please wait, you are being redirected to the Home page</h3>
</template>

<script>
import applicationUserManager from "../applicationusermanager";

export default {
  async created() {
    try {
      // completeSignIn(url);
      await applicationUserManager.signinRedirectCallback();
      this.$router.push({ name: "home" });
    } catch (e) {
      console.log(e);
    }
  },
  methods: {
    async completeSignIn(url) {
      await this.ensureUserManagerInitialized();
      try {
        const { state } = await this.userManager.readSigninResponseState(
          url,
          this.userManager.settings.stateStore
        );
        if (state.request_type === "si:r" || !state.request_type) {
          let user = await this.userManager.signinRedirectCallback(url);
          this.updateState(user);
          return this.success(state.data.userState);
        }
        if (state.request_type === "si:p") {
          await this.userManager.signinSilentCallback(url);
          return this.success(undefined);
        }
        if (state.request_type === "si:s") {
          await this.userManager.signinSilentCallback(url);
          return this.success(undefined);
        }

        throw new Error(`Invalid login mode '${state.request_type}'.`);
      } catch (signInResponseError) {
        console.log("There was an error signing in", signInResponseError);
        return this.error("Sing in callback authentication error.");
      }
    },
  },
};
</script>

<style>
</style>