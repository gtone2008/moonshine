function CheckFile(obj)//onchange="CheckFile(this);
{
    var array = new Array('gif', 'jpeg', 'png', 'jpg'); //可以上传的文件类型 
    //if (obj.value == '') {
    //    alert("选择要上传的图片!");
    //    return false;
    //}
    //else {
    var fileContentType = obj.value.match(/^(.*)(\.)(.{1,8})$/)[3]; //这个文件类型正则很有用：） 
    var isExists = false;
    for (var i in array) {
        if (fileContentType.toLowerCase() == array[i].toLowerCase()) {



            isExists = true;
            return true;

        }
    }
    if (isExists == false) {
        obj.value = null;
        alert("上传图片类型不正确!");
        return false;
    }
    return false;
    //}
};
function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}






