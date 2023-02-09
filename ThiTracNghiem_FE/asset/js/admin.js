const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);
const fromId = document.getElementById.bind(document);
const fromClass = document.getElementsByClassName.bind(document);
const fromTag = document.getElementsByTagName.bind(document);

var contentLeft = $("#leftSideBar");
var pageMonThi = fromId("pageMonThi");
var pageDeThi = fromId("pageDeThi");
var pageCauHoi = fromId("pageCauHoi");
var pageMaThi = fromId("pageMaThi");
var modalMonThi = fromId("insertMonThi");
var modalDeThi = fromId("insertDeThi");
var modalCauHoi = fromId("insertCauHoi");
var modalMaThi = fromId("insertMaThi");
var inputMonThi = fromId("tenMonThi");
var inputDeThi = $("#inputDeThi");
var tbodyDeThi = fromId("tbodyDeThi");
var tbodyMonThi = fromId("tbodyMonThi");
var tbodyMaThi = fromId("tbodyMaThi");
var modalDelete = $("#modalDelete");

// Tải danh sách môn thi
function getMonThis(index, search = "") {
  let url = `https://localhost:7002/api/MonThi/Page?pageNumber=${index}`;
  if (search) url += `&Text=${search}`
  fetch(url)
    .then((res) => {
      let header = JSON.parse(res.headers.get('X-Pagination'));
      sessionStorage.setItem("CurrentPage", header.CurrentPage);
      sessionStorage.setItem("HasNext", header.HasNext);
      sessionStorage.setItem("HasPrevious", header.HasPrevious);
      sessionStorage.setItem("PageSize", header.PageSize);
      sessionStorage.setItem("TotalCount", header.TotalCount);
      sessionStorage.setItem("TotalPages", header.TotalPages);
      return res.json();
    })
    .then((data) => displayMonThi(data))
    .catch((err) => console.log(err));
}
getMonThis(1, "");

// Tải danh sách đề thi
async function getListDeThi(index, search = "") {
  let url = `https://localhost:7002/api/DeThi/Page?pageNumber=${index}`;
  if (search) url += `&Text=${search}`
  await fetch(url)
    .then((res) => {
      let header = JSON.parse(res.headers.get('X-Pagination'));
      sessionStorage.setItem("CurrentPage", header.CurrentPage);
      sessionStorage.setItem("HasNext", header.HasNext);
      sessionStorage.setItem("HasPrevious", header.HasPrevious);
      sessionStorage.setItem("PageSize", header.PageSize);
      sessionStorage.setItem("TotalCount", header.TotalCount);
      sessionStorage.setItem("TotalPages", header.TotalPages);
      return res.json();
    })
    .then((data) => displayDeThi(data))
    .catch((err) => console.log(err));
}

// Hiển thị danh sách đề thi
const displayDeThi = (data) => {
  disibledButton("exam")
  tbodyDeThi.innerHTML = "";
  data.forEach((element) => {
    tbodyDeThi.innerHTML += `<tr>
        <td>${element.id}</td>
        <td>${element.tenDeThi}</td>
        <td>${element.soLuongCauHoi}</td>
        <td>${element.monThi.tenMonThi}</td>
        <td data-id="${element.id}">
            <button class="btnEditDeThi"><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M362.7 19.3L314.3 67.7 444.3 197.7l48.4-48.4c25-25 25-65.5 0-90.5L453.3 19.3c-25-25-65.5-25-90.5 0zm-71 71L58.6 323.5c-10.4 10.4-18 23.3-22.2 37.4L1 481.2C-1.5 489.7 .8 498.8 7 505s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L421.7 220.3 291.7 90.3z"/></svg></button>
            <button class="btnDeleteDeThi" ><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z"/></svg></button>
        </td>
    </tr>`;
  });
};

// Tải danh sách câu hỏi
async function getListCauHoi(index, search = "") {
  let url = `https://localhost:7002/api/CauHoi/Page?PageNumber=${index}`;
  if (search) url += `&Text=${search}`
  await fetch(url)
    .then((res) => {
      let header = JSON.parse(res.headers.get('X-Pagination'));
      sessionStorage.setItem("CurrentPage", header.CurrentPage);
      sessionStorage.setItem("HasNext", header.HasNext);
      sessionStorage.setItem("HasPrevious", header.HasPrevious);
      sessionStorage.setItem("PageSize", header.PageSize);
      sessionStorage.setItem("TotalCount", header.TotalCount);
      sessionStorage.setItem("TotalPages", header.TotalPages);
      return res.json();
    })
    .then((data) => displayCauHoi(data))
    .catch((err) => console.log(err));
}

// Hiển thị danh sách câu hỏi
const displayCauHoi = async (data) => {
  disibledButton("question")
  const tbodyCauHoi = $("#tbodyCauHoi");
  tbodyCauHoi.innerHTML = "";

  data.forEach((element) => {
    let dapAnDung = "";
    switch (element.dapAnDung) {
      case 1:
        dapAnDung = "A";
        break;
      case 2:
        dapAnDung = "B";
        break;
      case 3:
        dapAnDung = "C";
        break;
      case 4:
        dapAnDung = "D";
        break;
    }

    tbodyCauHoi.innerHTML += ` <tr>
        <td>${element.id}</td>
        <td>${element.noiDung}</td>
        <td>${element.a}</td>
        <td>${element.b}</td>
        <td>${element.c}</td>
        <td>${element.d}</td>
        <td>${dapAnDung}</td>
        <td>${element.deThi.tenDeThi}</td>
        <td data-id="${element.id}">
            <button hidden class="btnShowCauHoi"><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20"
                    viewBox="0 0 576 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. -->
                    <path
                        d="M288 32c-80.8 0-145.5 36.8-192.6 80.6C48.6 156 17.3 208 2.5 243.7c-3.3 7.9-3.3 16.7 0 24.6C17.3 304 48.6 356 95.4 399.4C142.5 443.2 207.2 480 288 480s145.5-36.8 192.6-80.6c46.8-43.5 78.1-95.4 93-131.1c3.3-7.9 3.3-16.7 0-24.6c-14.9-35.7-46.2-87.7-93-131.1C433.5 68.8 368.8 32 288 32zM432 256c0 79.5-64.5 144-144 144s-144-64.5-144-144s64.5-144 144-144s144 64.5 144 144zM288 192c0 35.3-28.7 64-64 64c-11.5 0-22.3-3-31.6-8.4c-.2 2.8-.4 5.5-.4 8.4c0 53 43 96 96 96s96-43 96-96s-43-96-96-96c-2.8 0-5.6 .1-8.4 .4c5.3 9.3 8.4 20.1 8.4 31.6z" />
                </svg></button>
            <button class="btnEditCauHoi"><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20"
                    viewBox="0 0 512 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. -->
                    <path
                        d="M362.7 19.3L314.3 67.7 444.3 197.7l48.4-48.4c25-25 25-65.5 0-90.5L453.3 19.3c-25-25-65.5-25-90.5 0zm-71 71L58.6 323.5c-10.4 10.4-18 23.3-22.2 37.4L1 481.2C-1.5 489.7 .8 498.8 7 505s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L421.7 220.3 291.7 90.3z" />
                </svg></button>
            <button class="btnDeleteCauHoi"><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20"
                    viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. -->
                    <path
                        d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z" />
                </svg></button>
        </td>
    </tr>`;
  });
};

const disibledButton = (name) => {
  const hasPrev = sessionStorage.getItem("HasPrevious");
  if (hasPrev == "false") $(`#prev-page-${name}`).disabled = true
  else $(`#prev-page-${name}`).disabled = false

  const hasNext = sessionStorage.getItem("HasNext");
  if (hasNext == "false") $(`#next-page-${name}`).disabled = true
  else $(`#next-page-${name}`).disabled = false
}

// Tải môn thi theo Id
async function getMonThiById(Id) {
  const response = await fetch(`https://localhost:7002/api/MonThi/${Id}`)
    .then((res) => res.json())
    .catch((er) => {
      alert(er);
      console.log(er);
    });
  return response;
}

// Hiển thị dữ liệu lên table
const displayMonThi = async (data) => {
  disibledButton("subject")
  tbodyMonThi.innerHTML = "";
  data.forEach(async (element) => {
    tbodyMonThi.innerHTML += `<tr>
        <td>${element.id}</td>
        <td>${element.tenMonThi}</td>
        <td>${element.soLuongDe}</td>
        <td data-id="${element.id}">
            <button class="btnEditMonThi"><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M362.7 19.3L314.3 67.7 444.3 197.7l48.4-48.4c25-25 25-65.5 0-90.5L453.3 19.3c-25-25-65.5-25-90.5 0zm-71 71L58.6 323.5c-10.4 10.4-18 23.3-22.2 37.4L1 481.2C-1.5 489.7 .8 498.8 7 505s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L421.7 220.3 291.7 90.3z"/></svg></button>
            <button class="btnDeleteMonThi" ><svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z"/></svg></button>
        </td>
    </tr>`;
  });
};

// Tải danh sách mã thi
async function getListMaThi(index, search = "") {
  let url = "https://localhost:7002/api/mathi/Page?pageNumber=" + index;
  if (search) url += `&Text=${search}`
  await fetch(url)
    .then((res) => {
      let header = JSON.parse(res.headers.get('X-Pagination'));
      sessionStorage.setItem("CurrentPage", header.CurrentPage);
      sessionStorage.setItem("HasNext", header.HasNext);
      sessionStorage.setItem("HasPrevious", header.HasPrevious);
      sessionStorage.setItem("PageSize", header.PageSize);
      sessionStorage.setItem("TotalCount", header.TotalCount);
      sessionStorage.setItem("TotalPages", header.TotalPages);
      return res.json();
    })
    .then((data) => displayMaThi(data))
    .catch((err) => console.log(err));
}

// Hiển thị danh sách mã thi
function displayMaThi(data) {
  disibledButton("code")
  let html = [];
  data.forEach((element) => {
    let text = `<tr>
        <td>${element.ma}</td>
        <td>${element.slsd}/5</td>
        <td hidden>
        <button class="btnDeleteMaThi" >
            <svg
            xmlns="http://www.w3.org/2000/svg"
            height="20"
            width="20"
            viewBox="0 0 448 512"
            >
            <!--! Font Awesome Pro 6.2.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. -->
            <path
                d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0H284.2c12.1 0 23.2 6.8 28.6 17.7L320 32h96c17.7 0 32 14.3 32 32s-14.3 32-32 32H32C14.3 96 0 81.7 0 64S14.3 32 32 32h96l7.2-14.3zM32 128H416V448c0 35.3-28.7 64-64 64H96c-35.3 0-64-28.7-64-64V128zm96 64c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16V432c0 8.8 7.2 16 16 16s16-7.2 16-16V208c0-8.8-7.2-16-16-16z"
            />
            </svg>
        </button>
        </td>
    </tr>`;

    html.push(text);
  });
  tbodyMaThi.innerHTML = html.join("");
}

//Lắng nghe left sidebar
contentLeft.addEventListener("click", (e) => {
  const contentItems = $$(".content-left_item");
  if (e.target.classList.contains("content-left_item")) {
    if (e.target.classList.contains("active") == false) {
      contentItems.forEach((element) => {
        if (element !== e.target) {
          element.classList.remove("active");
        } else {
          element.classList.add("active");
        }
      });

      if (e.target.innerText === "Quản lý môn thi") {
        pageDeThi.style.display = "none";
        pageCauHoi.style.display = "none";
        pageMaThi.style.display = "none";
        pageMonThi.style.display = "block";
        getMonThis(1, "");
      } else if (e.target.innerText === "Quản lý đề thi") {
        pageDeThi.style.display = "block";
        pageCauHoi.style.display = "none";
        pageMaThi.style.display = "none";
        pageMonThi.style.display = "none";
        getListDeThi(1, "");
      } else if (e.target.innerText === "Quản lý câu hỏi") {
        pageDeThi.style.display = "none";
        pageCauHoi.style.display = "block";
        pageMaThi.style.display = "none";
        pageMonThi.style.display = "none";
        getListCauHoi(1, "");
      } else if (e.target.innerText === "Quản lý mã thi") {
        pageDeThi.style.display = "none";
        pageCauHoi.style.display = "none";
        pageMaThi.style.display = "block";
        pageMonThi.style.display = "none";
        getListMaThi(1, "");
      }
    }
  }
});

// Xủ lý page môn thi
pageMonThi.addEventListener("click", async (e) => {
  if (e.target.closest(".btnAddMonThi") !== null) {
    modalMonThi.children[1].children[1].innerText = "Thêm môn thi";
    modalMonThi.children[1].lastElementChild.firstElementChild.innerText =
      "Thêm";
    modalMonThi.classList.toggle("active");
  }

  if (e.target.closest(".btnEditMonThi") !== null) {
    let item = await getMonThiById(
      e.target.closest(".btnEditMonThi").parentElement.dataset.id
    );
    inputMonThi.value = item.tenMonThi;
    inputMonThi.dataset.id = item.id;
    modalMonThi.children[1].children[1].innerText = "Sửa môn thi";
    modalMonThi.children[1].lastElementChild.firstElementChild.innerText =
      "Sửa";
    modalMonThi.classList.add("active");
  }

  if (e.target.closest(".btnDeleteMonThi") !== null) {
    modalDelete.classList.add("active");
    modalDelete.dataset.id =
      e.target.closest(".btnDeleteMonThi").parentElement.dataset.id;
    modalDelete.dataset.type = "monthi";
  }
});

const themMonThi = () => {
  if (inputMonThi.value !== "") {
    createMonThi(inputMonThi.value);
    inputMonThi.value = "";
    modalMonThi.classList.remove("active");
  } else {
    var parent = inputMonThi.closest(".typingBox");
    parent.classList.add("typingBox--error");
    inputMonThi.parentElement.lastElementChild.innerText =
      "Vui lòng nhập đẩy đủ thông tin";
  }
};

// Xử lý modal môn thi
modalMonThi.addEventListener("click", (e) => {
  if (
    e.target.classList.contains("close") ||
    e.target.classList.contains("modal__overlay") ||
    e.target.innerText == "Hủy"
  ) {
    modalMonThi.classList.remove("active");
    inputMonThi.value = "";
    inputMonThi.closest(".typingBox").classList.remove("typingBox--error");
  }

  if (e.target.innerText == "Thêm") {
    themMonThi();
  }

  if (e.target.innerText == "Sửa") {
    if (inputMonThi.value !== "") {
      updateMonThi(inputMonThi.dataset.id, inputMonThi.value);
      inputMonThi.value = "";
      modalMonThi.classList.remove("active");
    } else {
      var parent = inputMonThi.closest(".typingBox");
      parent.classList.add("typingBox--error");
      inputMonThi.parentElement.lastElementChild.innerText =
        "Vui lòng nhập đẩy đủ thông tin";
    }
  }
});

// inputMonThi lắng nghe sự kiện
var listenIpMonThi = inputMonThi.addEventListener("focus", () => {
  let parent = inputMonThi.closest(".typingBox");
  if (parent.classList.contains("typingBox--error")) {
    parent.classList.remove("typingBox--error");
  }
});

// Xử lý page Câu hỏi
pageCauHoi.addEventListener("click", async (e) => {
  if (e.target.closest(".btnAddCauHoi") != null) {
    // Lấy dư liệu cho thẻ selection
    let data = await fetch(
      "https://localhost:7002/api/DeThi/GetDeThiWithName"
    ).then((res) => res.json());
    let html = [];
    html.push(`<option value="-1">-- Chọn đề thi --</option>`);
    data.forEach((element) => {
      html.push(
        `<option value="${element.id}">${element.tenDeThi} (${element.tenMonThi})</option>`
      );
    });
    $("#selectDeThi").innerHTML = html.join("");
    modalCauHoi.children[1].children[1].innerText = "Thêm câu hỏi";
    modalCauHoi.children[1].lastElementChild.firstElementChild.innerText =
      "Thêm";
    modalCauHoi.classList.add("active");
  }

  if (e.target.closest(".btnEditCauHoi") != null) {
    const idCauHoi =
      e.target.closest(".btnEditCauHoi").parentElement.dataset.id;
    const cauHoi = await fetch(
        `https://localhost:7002/api/cauhoi/${idCauHoi}`, {
          method: "GET",
        }
      )
      .then((res) => res.json())
      .then((data) => data)
      .catch((err) => console.log(err));
    $("#noiDung").value = cauHoi.noiDung;
    $("#dapAnA").value = cauHoi.a;
    $("#dapAnB").value = cauHoi.b;
    $("#dapAnC").value = cauHoi.c;
    $("#dapAnD").value = cauHoi.d;
    let dapAnOptions = $("#selectDapAn").children;
    let deThiOptions = $("#selectDeThi").children;

    for (let i = 0; i < dapAnOptions.length; i++) {
      if (dapAnOptions[i].value == cauHoi.dapAnDung) {
        dapAnOptions[i].selected = true;
      }
    }

    const danhSachDeThi = await fetch(
        "https://localhost:7002/api/dethi/GetDeThiWithName"
      )
      .then((res) => res.json())
      .then((data) => data)
      .catch((err) => console.log(err));
    let html = [];
    html.push(`<option value="-1">-- Chọn đề thi --</option>`);
    danhSachDeThi.forEach((element) => {
      html.push(
        `<option value="${element.id}">${element.tenDeThi} (${element.tenMonThi})</option>`
      );
    });
    $("#selectDeThi").innerHTML = html.join("");

    for (let i = 0; i < deThiOptions.length; i++) {
      if (deThiOptions[i].value == cauHoi.deThiID) {
        deThiOptions[i].selected = true;
      }
    }
    modalCauHoi.children[1].children[1].innerText = "Sửa câu hỏi";
    modalCauHoi.children[1].lastElementChild.firstElementChild.innerText =
      "Sửa";
    modalCauHoi.dataset.id = idCauHoi;
    modalCauHoi.classList.add("active");
  }

  if (e.target.closest(".btnDeleteCauHoi") != null) {
    modalDelete.dataset.id =
      e.target.closest(".btnDeleteCauHoi").parentElement.dataset.id;
    modalDelete.dataset.type = "cauhoi";
    modalDelete.classList.add("active");
  }
});

const clearError = () => {
  $("#text-alert-question").parentNode.classList.remove("typingBox--error");
};

const clearDataQuestion = () => {
  $("#noiDung").value = "";
  $("#dapAnA").value = "";
  $("#dapAnB").value = "";
  $("#dapAnC").value = "";
  $("#dapAnD").value = "";
  $("#selectDapAn").children[0].selected = true;
  $("#selectDeThi").children[0].selected = true;
};

// Xử lý modal Câu hỏi
modalCauHoi.addEventListener("click", async (e) => {
  if (
    e.target.classList.contains("close") ||
    e.target.classList.contains("modal__overlay") ||
    e.target.innerText == "Hủy"
  ) {
    modalCauHoi.classList.remove("active");
    clearDataQuestion();
  }

  if (e.target.innerText == "Thêm") {
    let noiDung = $("#noiDung").value;
    let dapAnA = $("#dapAnA").value;
    let dapAnB = $("#dapAnB").value;
    let dapAnC = $("#dapAnC").value;
    let dapAnD = $("#dapAnD").value;
    let dapAnOptions = $("#selectDapAn").children;
    let dapAnDung = null;
    let deThiOptions = $("#selectDeThi").children;
    let deThiID = null;
    for (let i = 0; i < dapAnOptions.length; i++) {
      if (dapAnOptions[i].value != -1 && dapAnOptions[i].selected == true) {
        dapAnDung = dapAnOptions[i].value;
      }
    }

    for (let i = 0; i < deThiOptions.length; i++) {
      if (deThiOptions[i].value != -1 && deThiOptions[i].selected == true) {
        deThiID = deThiOptions[i].value;
      }
    }

    if (
      !deThiID ||
      !noiDung ||
      !dapAnA ||
      !dapAnB ||
      !dapAnC ||
      !dapAnD ||
      !dapAnDung
    ) {
      $("#text-alert-question").parentNode.classList.add("typingBox--error");
      return;
    }
    const deThi = {
      noiDung,
      a: dapAnA,
      b: dapAnB,
      c: dapAnC,
      d: dapAnD,
      dapAnDung: parseInt(dapAnDung),
      deThiID,
    };
    await fetch("https://localhost:7002/api/cauhoi", {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(deThi),
      })
      .then((res) => res.json())
      .then(() => {
        modalCauHoi.classList.remove("active");
        getListCauHoi(1, "");
        clearDataQuestion();
      })
      .catch((err) => console.log(err));
  }
  if (e.target.innerText == "Sửa") {
    const id = modalCauHoi.dataset.id;
    let noiDung = $("#noiDung").value;
    let dapAnA = $("#dapAnA").value;
    let dapAnB = $("#dapAnB").value;
    let dapAnC = $("#dapAnC").value;
    let dapAnD = $("#dapAnD").value;
    let dapAnOptions = $("#selectDapAn").children;
    let dapAnDung = null;
    let deThiOptions = $("#selectDeThi").children;
    let deThiID = null;
    for (let i = 0; i < dapAnOptions.length; i++) {
      if (dapAnOptions[i].value != -1 && dapAnOptions[i].selected == true) {
        dapAnDung = dapAnOptions[i].value;
      }
    }

    for (let i = 0; i < deThiOptions.length; i++) {
      if (deThiOptions[i].value != -1 && deThiOptions[i].selected == true) {
        deThiID = deThiOptions[i].value;
      }
    }

    if (
      !deThiID ||
      !noiDung ||
      !dapAnA ||
      !dapAnB ||
      !dapAnC ||
      !dapAnD ||
      !dapAnDung
    ) {
      $("#text-alert-question").parentNode.classList.add("typingBox--error");
      return;
    }
    const deThi = {
      id: id,
      noiDung,
      a: dapAnA,
      b: dapAnB,
      c: dapAnC,
      d: dapAnD,
      dapAnDung: parseInt(dapAnDung),
      deThiID,
    };
    await fetch("https://localhost:7002/api/cauhoi/" + id, {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(deThi),
      })
      .then(() => {
        modalCauHoi.classList.remove("active");
        getListCauHoi(1, "");
        clearDataQuestion();
      })
      .catch((err) => console.log(err));
  }
  if (e.target.innerText == "Hủy") {
    clearDataQuestion();
  }
});

// Xử lý page Câu hỏi
pageMaThi.addEventListener("click", async (e) => {
  if (e.target.closest(".btnAddMaThi") != null) {
    modalMaThi.classList.add("active");
  }
});

// Xử lý page mã thi
modalMaThi.addEventListener("click", async (e) => {
  if (
    e.target.classList.contains("close") ||
    e.target.classList.contains("modal__overlay") ||
    e.target.innerText == "Thoát"
  ) {
    modalMaThi.classList.remove("active");
    getListMaThi(1, "");
    const listCodeArea = fromId("listCode");
    listCodeArea.value = "";
    fromId("numberCode").value = 1;
  }

  if (e.target.innerText == "Tạo mã") {
    const numberCode = parseInt(fromId("numberCode").value);
    const listCodeArea = fromId("listCode");
    const listCode = [];
    for (let index = 0; index < numberCode; index++) {
      await fetch("https://localhost:7002/api/mathi", {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        })
        .then((res) => res.json())
        .then((data) => {
          listCode.push(data);
          listCodeArea.value = listCodeArea.value + data.ma + "\n";
        })
        .catch((err) => console.log(err));
    }
  }
});

const clearDataInputDeThi = () => {
  let html = [];
  html.push('<option value="-1">-- Chọn môn thi --</option>');
  $("#selectMonThi").innerHTML = html.join("");
  $("#selectMonThi").children[0].selected;
  inputDeThi.value = "";
  fileUpload.value = "";
  modalDeThi.classList.remove("active");
};

// Xử lý page Đề Thi
pageDeThi.addEventListener("click", async (e) => {
  if (e.target.closest(".btnAddDeThi") != null) {
    $("#fileUpload").parentNode.classList.remove("none")
    const listMonThi = await fetch(`https://localhost:7002/api/monthi`)
      .then((res) => res.json())
      .catch((err) => console.log(err));

    let html = [];
    html.push('<option value="-1">-- Chọn môn thi --</option>');
    listMonThi.forEach((element) => {
      html.push(`<option value="${element.id}">${element.tenMonThi}</option>`);
    });
    $("#selectMonThi").innerHTML = html.join("");
    modalDeThi.children[1].children[1].innerText = "Thêm đề thi";
    modalDeThi.children[1].lastElementChild.firstElementChild.innerText = "Thêm";
    modalDeThi.classList.add("active");
  }

  if (e.target.closest(".btnEditDeThi") !== null) {
    const idDeThi = e.target.closest(".btnEditDeThi").parentElement.dataset.id;
    const deThi = await fetch(`https://localhost:7002/api/dethi/${idDeThi}`, {
        method: "GET",
      })
      .then((res) => res.json())
      .then((data) => data)
      .catch((err) => console.log(err));

    $("#inputDeThi").value = deThi.tenDeThi;
    const listMonThi = await fetch(`https://localhost:7002/api/monthi`)
      .then((res) => res.json())
      .catch((err) => console.log(err));

    let html = [];
    html.push('<option value="-1">-- Chọn môn thi --</option>');
    listMonThi.forEach((element) => {
      html.push(`<option value="${element.id}">${element.tenMonThi}</option>`);
    });
    $("#selectMonThi").innerHTML = html.join("");

    let monThiOptions = $("#selectMonThi").children;
    for (let i = 0; i < monThiOptions.length; i++) {
      if (monThiOptions[i].value == deThi.monThiID) {
        monThiOptions[i].selected = true;
      }
    }
    modalDeThi.children[1].children[1].innerText = "Sửa đề thi";
    modalDeThi.children[1].lastElementChild.firstElementChild.innerText = "Sửa";
    modalDeThi.dataset.id = idDeThi;
    $("#fileUpload").parentNode.classList.add("none");
    modalDeThi.classList.add("active");
  }

  if (e.target.closest(".btnDeleteDeThi") != null) {
    modalDelete.dataset.id =
      e.target.closest(".btnDeleteDeThi").parentElement.dataset.id;
    modalDelete.dataset.type = "dethi";
    modalDelete.classList.add("active");
  }
});

// Xử lý modal Đề Thi
modalDeThi.addEventListener("click", async (e) => {
  if (
    e.target.classList.contains("close") ||
    e.target.classList.contains("modal__overlay") ||
    e.target.innerText == "Hủy"
  ) {
    clearDataInputDeThi();
  }

  if (e.target.innerText == "Thêm") {
    UploadProcess();
  }

  if (e.target.innerText == "Sửa") {
    const id = modalDeThi.dataset.id;
    const tenDeThi = inputDeThi.value;
    const monThiID =
      $("#selectMonThi").children[$("#selectMonThi").selectedIndex].value;

    if (monThiID == -1 || String(tenDeThi).length == 0) {
      $("#textAlertDeThi").parentElement.classList.add("typingBox--error");
      $("#textAlertDeThi").innerText = "Vui lòng nhập đầy đủ các trường";
      return;
    }

    const deThi = {
      id,
      tenDeThi,
      monThiID,
    };

    await fetch("https://localhost:7002/api/dethi/" + id, {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(deThi),
      })
      .then(() => {
        modalDeThi.classList.remove("active");
        getListDeThi(1, "");
        clearDataInputDeThi();
      })
      .catch((err) => console.log(err));
  }
});

// modalDelete lắng nghe sự kiện click
modalDelete.addEventListener("click", async (e) => {
  if (
    e.target.classList.contains("close") ||
    e.target.classList.contains("modal__overlay") ||
    e.target.innerText == "Hủy"
  ) {
    modalDelete.classList.remove("active");
  }

  if (e.target.innerText == "Đồng ý") {
    await deleteItem(modalDelete.dataset.id, modalDelete.dataset.type);
    modalDelete.classList.remove("active");
  }
});

//Tạo môn thi
async function createMonThi(tenMonThi) {
  item = {
    tenMonThi: tenMonThi,
  };
  await fetch("https://localhost:7002/api/MonThi", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
        mode: "no-cors",
      },
      body: JSON.stringify(item),
    })
    .then((res) => res.json)
    .then((data) => {
      alert("Thêm môn thi thành công!!");
    })
    .catch((er) => {
      alert("Thêm môn thi thất bại");
    });

  await getMonThis(1, "");
}

//Sửa môn thi
async function updateMonThi(Id, tenMonThi) {
  let item = {
    id: Id,
    tenMonThi: tenMonThi,
  };

  await fetch(`https://localhost:7002/api/MonThi/${Id}`, {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(item),
    })
    .then(() => getMonThis(1, ""))
    .catch((er) => console.error("Không thể cập nhật mục", er));
}

// Xóa mục
async function deleteItem(Id, type) {
  await fetch(`https://localhost:7002/api/${type}/${Id}`, {
      method: "DELETE",
    })
    .then(() => {
      getMonThis(1, "");
      getListCauHoi(1, "");
      getListDeThi(1, "");
    })
    .catch((er) => console.error("Không thể xóa mục", er));
}

// Lấy file up lên server
function UploadProcess() {
  //Reference the FileUpload element.
  var fileUpload = document.getElementById("fileUpload");

  //Validate whether File is valid Excel file.
  var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$/;
  if (regex.test(fileUpload.value.toLowerCase())) {
    if (typeof FileReader != "undefined") {
      var reader = new FileReader();

      //For Browsers other than IE.
      if (reader.readAsBinaryString) {
        reader.onload = function (e) {
          ConvertExcelToJson(e.target.result);
        };
        reader.readAsBinaryString(fileUpload.files[0]);
      } else {
        //For IE Browser.
        reader.onload = function (e) {
          var data = "";
          var bytes = new Uint8Array(e.target.result);
          for (var i = 0; i < bytes.byteLength; i++) {
            data += String.fromCharCode(bytes[i]);
          }
          ConvertExcelToJson(e.target.result);
        };
        reader.readAsArrayBuffer(fileUpload.files[0]);
      }
    } else {
      alert("Trình duyệt của bạn không hỗ trợ HTML5.");
    }
  } else {
    createExamHandle();
  }
}

const createExamHandle = async () => {
  const deThiCreated = await createExam().then((data) => data);
  if (deThiCreated != null) getListDeThi(1, "");
};

const createExam = async () => {
  const tenDeThi = inputDeThi.value;
  const monThiID =
    $("#selectMonThi").children[$("#selectMonThi").selectedIndex].value;

  if (monThiID == -1 || String(tenDeThi).length == 0) {
    $("#textAlertDeThi").parentElement.classList.add("typingBox--error");
    $("#textAlertDeThi").innerText = "Vui lòng nhập đầy đủ các trường";
    return;
  }

  const deThi = {
    tenDeThi,
    monThiID,
  };

  // Thêm đề thi
  const deThiCreated = await fetch(`https://localhost:7002/api/dethi`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(deThi),
    })
    .then((res) => res.json())
    .catch((err) => console.log(err));
  if (deThiCreated != null) {
    clearDataInputDeThi();
    return deThiCreated;
  }
  return;
};

// Chuyển file sang dạng Json
function ConvertExcelToJson(data) {
  try {
    var workbook = XLSX.read(data, {
      type: "binary",
    });
    var first_ws = workbook.Sheets[workbook.SheetNames[0]];
    var XL_row_object = XLSX.utils.sheet_to_row_object_array(first_ws);

    // Nếu row của sheet đủ 6 cột
    if (Object.keys(XL_row_object[0]).length == 6) {
      createDeThi(XL_row_object);
    } else {
      alert("Định dạng file excel không hợp lệ!!");
      return;
    }
  } catch (err) {
    alert("Lỗi thư viện XLSX!!!");
    console.log(err);
  }
}

// Tạo đề thi với danh sách câu hỏi
async function createDeThi(listQuestion) {
  let listQuestionPost = [];
  for (let i = 0; i < listQuestion.length; ++i) {
    let arrayValue = Object.values(listQuestion[i]);
    const questionItem = {
      noiDung: arrayValue[0],
      a: arrayValue[1],
      b: arrayValue[2],
      c: arrayValue[3],
      d: arrayValue[4],
      dapAnDung: convertStringToNum(arrayValue[5]),
      dethiID: -1,
    };

    // Kiểm tra trường đáp án đúng
    if (questionItem.dapAnDung == 0) {
      alert(
        "Trường đáp án đúng không hợp lệ\nVui lòng xem lại định dạng file excel"
      );
      return;
    }

    listQuestionPost.push(questionItem);
  }

  // Thêm đề thi
  const deThiCreated = await createExam().then((data) => data);

  if (deThiCreated != null) {
    for (let i = 0; i < listQuestionPost.length; ++i) {
      listQuestionPost[i].dethiID = deThiCreated.id;
    }
  } else {
    alert("Tạo đề thi thất bại!!");
    return;
  }

  if (listQuestionPost !== null) {
    await fetch(`https://localhost:7002/api/CauHoi/PostListQuestion`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(listQuestionPost),
    }).catch((err) => console.log(err));
    getListDeThi(1, "");
  } else {
    alert("Danh sách câu hỏi không hợp lệ");
    return;
  }
}

// Chuyển đáp án từ chuỗi sang số
function convertStringToNum(str) {
  if (str.toLowerCase() === "a") {
    return 1;
  }
  if (str.toLowerCase() === "b") {
    return 2;
  }
  if (str.toLowerCase() === "c") {
    return 3;
  }
  if (str.toLowerCase() === "d") {
    return 4;
  }

  return 0;
}

function clearErrorDeThi() {
  $("#textAlertDeThi").parentElement.classList.remove("typingBox--error");
  $("#textAlertDeThi").innerText = "";
}

const checkAdmin = async () => {
  const username = getCookie("username");
  const password = getCookie("password");
  if (username && password) {
    const admin = {
      username,
      password,
    };
    await fetch(`https://localhost:7002/api/admin`, {
        method: "POST",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(admin),
      })
      .then((res) => res.json())
      .then((data) => {
        if (!data) window.location = "./login.html";
      })
      .catch(() => {
        window.location = "./login.html";
      });
  } else {
    window.location = "./login.html";
  }
};

const getCookie = (cname) => {
  let name = cname + "=";
  let decodedCookie = decodeURIComponent(document.cookie);
  let ca = decodedCookie.split(";");
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == " ") {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
};

const clearCookie = () => {
  const username = getCookie("username");
  const password = getCookie("password");
  const date = new Date();
  date.setTime(date.getTime() - 7 * 24 * 60 * 60 * 1000);
  const expires = "; expires=" + date.toUTCString();
  document.cookie = `username=${username}; ${expires}; ; path=/`;
  document.cookie = `password=${password}; ${expires}; ; path=/`;
  setTimeout(() => {
    window.location = "./login.html";
  }, 500);
};

const pageChange = (num, name) => {
  let currentPage = parseInt(sessionStorage.getItem("CurrentPage"))
  if (num == -1) {
    const hasPrev = sessionStorage.getItem("HasPrevious");
    if (hasPrev == "true") {
      currentPage += -1;
    }
  } else {
    const hasNext = sessionStorage.getItem("HasNext");
    if (hasNext == "true") {
      currentPage += 1
    }
  }


  if (name == "subject") {
    const search = $("#search-subject").value.trim();
    getMonThis(currentPage, search)
  }
  if (name == "exam") {
    const search = $("#search-exam").value.trim();
    getListDeThi(currentPage, search)
  }
  if (name == "question") {
    const search = $("#search-question").value.trim();
    getListCauHoi(currentPage, search);
  }
  if (name == "code") {
    const search = $("#search-code").value.trim();
    getListMaThi(currentPage, search);
  }
}

function copyMaThi(){
  const texetArea = $('#listCode');
  texetArea.select();
  texetArea.setSelectionRange(0, 99999);
  navigator.clipboard.writeText(texetArea.value.trim());

}