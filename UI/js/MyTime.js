$.extend({
    convertJonsTime: function (time) {

        if (time == "" || time == null)
            return "";
        var str = parseFloat(time.substr(6));
        var t = new Date(str);
        return t.getFullYear() + "-" + (t.getMonth() + 1) + "-" + t.getDate();


    }
})