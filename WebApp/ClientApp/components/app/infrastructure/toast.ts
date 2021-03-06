import 'izitoast/dist/css/iziToast.min.css'
import iZToast from "izitoast";

// Motivated by https://www.qcode.in/api-error-handling-in-vue-with-axios/
const toast = {
    error: (message : string, title = 'Error') => {
        return iZToast.error({
            title: title,
            message: message,
            position: 'bottomCenter'
        });
    },
    success: (message: string, title = 'Success') => {
        return iZToast.success({
            title: title,
            message: message,
            position: 'bottomCenter'
        });
    },
    
    onSuccess: (response: any, message: string, title = 'Success') => {
        if (response) {
            toast.success(message, title);
        }
    }
};

export default toast;