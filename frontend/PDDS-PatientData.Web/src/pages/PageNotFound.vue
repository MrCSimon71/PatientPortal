<template>
  <div id="notfound" class="page-container center-content">
    <div style="width:100%; align-content: center">
      <img src="@/assets/images/404-not-found.png" class="notfound-img" />
      <div class="col-xs-4">
        <router-link to="/dashboard" style="margin-right: 10px">
          <button type="button" class="btn btn-primary btn-lg">Return to Dashboard</button>
        </router-link>
        <router-link to="/">
          <button type="button" class="btn btn-primary btn-lg">Go Home</button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>

</script>

<style scoped>
  #notfound img {
      width: 540px;
      height: 425px;
      margin-bottom: 20px;
  }

  #notfound a {
      /*display: block;*/
  }

  #notfound button {
      display: inline-block;
  }
</style>