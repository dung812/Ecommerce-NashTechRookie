export const CompactText =(text, maxlength = 15) => {
    var result = "";

    if (text != null) {
        if (text.length <= maxlength)
            return text;

        else {
            for (var i = 0; i < maxlength; i++) {
                result = result.concat(text.split('')[i])
            }
            return result + "...";
        }
    }
    else
        return result;
}


export const FormatDateTime = (datetime) => {
    if (datetime != null) {
    let date = `${datetime.split("T")[0].split("-")[2]}-${datetime.split("T")[0].split("-")[1]}-${datetime.split("T")[0].split("-")[0]}`;
    let time = `${datetime.split("T")[1].split(":")[0]}:${datetime.split("T")[1].split(":")[1]}`;
    return `${date} ${time}`;
    }
    return "";
} 

export const formatDate = (date) => {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) 
        month = '0' + month;
    if (day.length < 2) 
        day = '0' + day;

    return [year, month, day].join('-');
}

export const UTCWithoutHour = (date) => {
    return new Date(
        Date.UTC(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
        )
    );
}
