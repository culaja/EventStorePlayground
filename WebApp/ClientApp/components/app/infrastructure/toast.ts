import 'izitoast/dist/css/iziToast.min.css'
import iZToast from "izitoast";

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
    }
};

export default toast;