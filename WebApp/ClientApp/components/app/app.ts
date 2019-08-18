import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from "axios";
import toast from "./infrastructure/toast";

@Component({
    components: {
        MenuComponent: require('../navmenu/navmenu.vue.html')
    }
})
export default class AppComponent extends Vue {
    mounted() {
        this.prepareRestErrorHandling();
    }
    
    private prepareRestErrorHandling() {
        axios.interceptors.response.use(
            response => response,
            error => {
                if (error.response) {
                    toast.error(error.response.data);
                }
            }
        )
    }
}
