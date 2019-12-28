function collapsibleMenuItem(handle) {

    var item;
    // boy, this got ugly, didn't it?
    for (var index in handle.childNodes) {
        var child = handle.childNodes[index];
        if (child.className && child.className.includes("collapsible")) {
            item = child;
            break;
        }
    }

    var grandParent = item.parentElement.parentElement;
    for (var index in grandParent.childNodes) {
        var child = grandParent.childNodes[index];

        for (var index2 in child.childNodes) {
            var grandChild = child.childNodes[index2];

            if (shouldCollapse(grandChild)) {
                grandChild.className += " collapsed";
            }
        }
    }

    item.className = item.className.replace(" collapsed", "");
}

function shouldCollapse(element) {

    return element.className
        && element.className.includes("collapsible")
        && !element.className.includes("collapsed");
}

// for now, we're only supporting two-step parentage, because YOLO
function regular_collapsibleMenuItem(item) {

    var parent = item.parentElement;
    for (var index in parent.childNodes) {
        var child = parent.childNodes[index];

        if (shouldCollapse(child)) {
            child.className += " collapsed";
        }
    }

    item.className = item.className.replace(" collapsed", "");
}
