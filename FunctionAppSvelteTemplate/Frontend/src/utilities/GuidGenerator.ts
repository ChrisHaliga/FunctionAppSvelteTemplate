export function generateGUID() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        let r:number = (Math.random() * 16) | 0;
        let v:number = c === 'x' ? r : (r & 0x3) | 0x8;
        return v.toString(16);
    });
}

  