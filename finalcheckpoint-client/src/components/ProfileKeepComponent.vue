<template>
  <div class="row mx-1">
    <div class="card"
         href=""
         data-toggle="modal"
         :data-target="'#keepsModal' + keepsProps.id"
         :id="keepsProps.id"
         @click="getAccount()"
    >
      <img class="card-img-top background-img img-fluid rounded" :src="keepsProps.img" alt="Card image" style="width:100%">
      <div class="card-img-overlay">
        <div class="row position-absolute fixed-bottom">
          <div class="col-12 text-right px-5">
            <i class="fa fa-trash text-danger" v-if="state.account.id == state.profile.id" @click="deleteKeep()" aria-hidden="true"></i>
          </div>
          <div class="col-8">
            <p class="keepName float-left text-wrap text-weight-bold ">
              <b>
                {{ keepsProps.name }}
              </b>
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="modal fade"
         :id="'keepsModal' + keepsProps.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <button type="button" class="close text-right" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <div class="row">
            <div class="col-6 imgModal pt-0 pb-0 ml-3 py-0">
              <img class="rounded img-fluid" :src="keepsProps.img" alt="" style="width:100%">
            </div>
            <div class="col-5 mr-5">
              <div class="row">
                <div class="col-1 offset-10 eye text-right d-flex">
                  <i class="fa fa-eye text-success mx-1 py-1 text-right" aria-hidden="true"></i><p>
                    {{ keepsProps.views }}
                  </p>
                </div>
                <div class="col-1 thumbtack d-flex text-left justify-content-left align-items-left">
                  <i class="fa fa-thumb-tack text-success py-1 mx-1" aria-hidden="true"></i>
                  <p> {{ keepsProps.keeps }} </p>
                </div>
              </div>
              <div class="col-12 py-5 text-center">
                <h1> {{ keepsProps.name }}</h1>
              </div>
              <div class="col-8 offset-2 text-weight-light">
                {{ keepsProps.description }}
              </div>
              <div class="col-12 border-bottom bold py-3">
              </div>
              <div class="col-12">
              </div>
              <div class="row bottomKeepModal">
                <div class="col-5 text-center">
                  <div class="dropdown  text-success border border-success">
                    <button class="btn toggler dropdown-toggle button text-success"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                    >
                      ADD TO VAULT:
                    </button>
                    <div class="dropdown-menu" aria-labelledby="drowpdownMenuButton">
                      <DropdownComponent v-for="vault in state.vaults" :key="vault.id" :vaults-props="vault" :keeps-props="keepsProps" />
                    </div>
                  </div>
                </div>
                <div class="col-1  text-right">
                  <i class="fa fa-trash fa-10x " v-if="state.account.id == keepsProps.creatorId" @click="deleteKeep()" aria-hidden="true"></i>
                </div>
                <div class="col-5 d-flex">
                  <router-link :to="{ name: 'ProfileDetailsPage', params: { id: keepsProps.creatorId}}">
                    <img class="card-img-profile justify-content-right align-items-right profile rounded" :src="keepsProps.creator.picture" alt="">
                  </router-link>
                  <h5 class="profileName ">
                    {{ keepsProps.creator.name }}
                  </h5>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
export default {
  name: 'ProfileKeepComponent',
  props: ['keepsProps'],
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile)
    })
    return {
      state,

      async deleteKeep(keepsProps) {
        const choice = confirm('Are you sure you want to delete this keep? It is irreversible')
        if (choice === true) {
          keepService.deleteKeep(props.keepsProps)
        } else {
          alert('Keep was not deleted')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

.profile {
  max-width: 50%;
  height: auto;
  margin-right: 10px;
  padding-bottom: 5px;
  opacity: 100%;
}
.keepName {
  opacity: 100%;
  color: #f9f9fb;
  margin-left: 15px;
}
.card-img-overlay {
  background-image: linear-gradient(#00000011,#0000008f);
  opacity: 95%;
}

.card {
  max-height: 100%
}
.bottomKeepModal{
  margin-left: 50px;
  margin-top: 260px;
  margin-bottom: 0px;
}
.modal {
  padding: 0;
}
.imgModal {
  padding: 0px;
  padding-left: 10px;
  padding-bottom: 30px;
}
.modal-dialog {
  max-width: 1400px;
  padding-top: 250px;
}

.eye{
  justify-content: right;
  align-items: right;
  margin-right: 0px;
  margin-left: 300px;
}
.profileName{
  display: inline-block;
}

// .card-columns {
//   @include media-breakpoint-only(lg) {
//     column-count:4;
//   }
//   @include media-breakpoint-only(x1){
//     column-count: 5;
//   }
// }
// body {
//   font-family: sans-serif;
//    margin: 0;
//    background: #f2f2f2;
// }

// h1 {
//   text-align: center;
//   margin-top: 50px;
// }

// p {
//   text-align: center;
//   margin-bottom:60px;
// }

// h4{
//   text-align:center;
//   line-height:80px;
//   font-weight:normal;

// }

// .card-title {
//   display: inline-block;
// }

// .card-img-overlay {
//   position: absolute;
//   bottom: 10px;
// }
// .masonry { /* Masonry container */
//     -webkit-column-count: 4;
//   -moz-column-count:4;
//   column-count: 4;
//   -webkit-column-gap: 1em;
//   -moz-column-gap: 1em;
//   column-gap: 1em;
//    margin: 1.5em;
//     padding: 0;
//     -moz-column-gap: 1.5em;
//     -webkit-column-gap: 1.5em;
//     column-gap: 1.5em;
//     font-size: .85em;
// }

// .card-img-top {
//   // max-width: 50%;
//   // height: auto;
//   display: block;
// }
// .item {
//     display: inline-block;
//     background: #fff;
//     padding: 1em;
//     margin: 0 0 1.5em;
//     width: 100%;
// -webkit-transition:1s ease all;
//     box-sizing: border-box;
//     -moz-box-sizing: border-box;
//     -webkit-box-sizing: border-box;
//     box-shadow: 2px 2px 4px 0 #ccc;
// }
// .item img{max-width:100%; height: auto;}

// @media only screen and (max-width: 320px) {
//     .masonry {
//         -moz-column-count: 1;
//         -webkit-column-count: 1;
//         column-count: 1;
//     }
// }

// @media only screen and (min-width: 321px) and (max-width: 768px){
//     .masonry {
//         -moz-column-count: 2;
//         -webkit-column-count: 2;
//         column-count: 2;
//     }
// }
// @media only screen and (min-width: 769px) and (max-width: 1200px){
//     .masonry {
//         -moz-column-count: 3;
//         -webkit-column-count: 3;
//         column-count: 3;
//     }
// }
// @media only screen and (min-width: 1201px) {
//     .masonry {
//         -moz-column-count: 4;
//         -webkit-column-count: 4;
//         column-count: 4;
//     }
// }
</style>
