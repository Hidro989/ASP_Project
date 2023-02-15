const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

// Login
const checkUserName = () => {
  const username = $("#floatingInput").value;
  if (username === "") return false;
  return true;
};

const checkPassword = () => {
  const password = $("#floatingPassword").value;
  if (password === "") return false;
  if (!password.match(/^[a-zA-Z0-9!@#$%^&*]{5,16}$/)) return false;
  return true;
};

const loginHandle = () => {
  const error = {
    username: false,
    password: false,
  };
  error.username = checkUserName();
  error.password = checkPassword();
  if (error.username && error.password) {
    login();
  } else showError(error);
};

const showError = (err) => {
  const formSignin = $("#form-signin");
  formSignin.classList.add("error");
  console.log(err);
};

const inputChange = () => {
  const formSignin = $("#form-signin");
  formSignin.classList.remove("error");
};

const login = async () => {
  const username = $("#floatingInput").value;
  const password = $("#floatingPassword").value;
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
      if (data) {
        const date = new Date();
        date.setTime(date.getTime() + 7 * 24 * 60 * 60 * 1000);
        const expires = "; expires=" + date.toUTCString();
        document.cookie = `username=${username}; ${expires}; ; path=/`;
        document.cookie = `password=${password}; ${expires}; ; path=/`;
        window.location.href = "./admin.html";
      }
    })
    .catch((err) => {
      console.log(err);
    });
};

// index
const subjectLoaded = async () => {
  const select = $("#exam-select");
  select.length = 1;
  examChange();

  const options = await fetch(`https://localhost:7002/api/monthi`, {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));

  options.forEach((option) => {
    const opt = document.createElement("option");
    opt.value = option.id;
    opt.text = option.tenMonThi;
    select.appendChild(opt);
  });
};

const examError = (err) => {
  const examForm = $("#exam-form");
  const errorText = $("#error-text");
  examForm.classList.add("error");
  errorText.innerText = err;
};

const examChange = () => {
  const examForm = $("#exam-form");
  examForm.classList.remove("error");
};

const examHandle = async () => {
  const code = $("#floatingInput").value;
  const select = $("#exam-select").value;

  if (code === "") {
    examError("Mã thi chưa nhập");
    return;
  }

  if (select === "") {
    examError("Môn học chưa được chọn");
    return;
  }

  const codeExam = await fetch(`https://localhost:7002/api/mathi/${code}`, {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));

  if (codeExam.status === 404) {
    examError("Mã thi không chính xác");
    return;
  }

  if (codeExam.slsd <= 0) {
    examError("Mã thi đã hết hạn");
    return;
  }

  const subjectExam = await fetch(
      `https://localhost:7002/api/monthi/${select}`, {
        method: "GET",
      }
    )
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));

  chooseExam(codeExam, subjectExam);
};

const chooseExam = async (code, subject) => {
  const listExam = await fetch("https://localhost:7002/api/dethi", {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));

  const exams = [];
  listExam.forEach((exam) => {
    if (exam.monThiID === subject.id) exams.push(exam);
  });
  if (listExam.length == 0) {
    examError("Hiện chưa có đề thi");
    return;
  }
  const randomExam = Math.floor(Math.random() * exams.length);
  sessionStorage.setItem("exam", exams[randomExam].id);
  sessionStorage.setItem("subject", subject.tenMonThi);
  sessionStorage.setItem("code", code.ma);
  window.location.href = "./home.html";
};

// Index
let x;
let minutes;
let seconds;
let topic;
let listQuestion;
let questions;
let running = false;
let questionIndexCurrent;

const checkCode = async () => {
  const code = sessionStorage.getItem("code");
  if (code) {
    const cd = await fetch(`https://localhost:7002/api/mathi/${code}`, {
        method: "GET",
      })
      .then((res) => res.json())
      .then((data) => data)
      .catch((err) => {
        console.log(err);
      });
    if(cd){
      await fetch(`https://localhost:7002/api/mathi/${code}`, {
      method: "PUT",
    }).catch((err) => console.log(err));
    }

    return cd.slsd > 0 ? true : false;
  }
};

const checkPermissions = async () => {
  const isCode = await checkCode();
  if (!isCode) {
    window.location.href = "./";
    clearDataLocal();
  }
  running = true;
  const idExam = sessionStorage.getItem("exam");
  if (!idExam) window.location.href = "./";
  const subject = sessionStorage.getItem("subject");
  const subjectExam = $("#subject-exam");
  const resultSubject = $("#relt-modal h2");
  subjectExam.innerText = subject;
  resultSubject.innerText = subject;
  topic = await fetch(`https://localhost:7002/api/dethi/${idExam}`, {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));
  if (topic) {
    const numQuestion = $("#num-question");
    const timeExam = $("#time-exam");
    const timeDown = $$(".time-down");
    numQuestion.innerText = topic.soLuongCauHoi + " câu";
    timeExam.innerText = topic.thoiGian + " phút";
    timeDown.forEach((time) => {
      time.innerText = topic.thoiGian + ":00";
    });
    loadQuestion(topic.id);
    timeCountDown(topic.thoiGian);
  }
};

const timeCountDown = (time) => {
  const timeDown = $$(".time-down");
  let countDownDate = new Date();
  countDownDate.setMinutes(countDownDate.getMinutes() + time);
  x = setInterval(function () {
    let now = new Date().getTime();
    let distance = countDownDate - now;
    minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    seconds = Math.floor((distance % (1000 * 60)) / 1000);
    if (minutes == 0 && seconds == 0) {
      clearInterval(x);
      resultHandle();
    }
    const minutesText = minutes < 10 ? "0" + minutes : minutes;
    const secondsText = seconds < 10 ? "0" + seconds : seconds;
    timeDown.forEach((time) => {
      time.innerText = minutesText + ":" + secondsText;
    });
  }, 1000);
};

const loadQuestion = async (id) => {
  listQuestion = await fetch("https://localhost:7002/api/cauhoi", {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));

  questions = [];
  listQuestion.forEach((question) => {
    if (question.deThiID === id) questions.push(question);
  });
  if (questions.length > 0) {
    rightAnswerLoading(questions);
    subQuestionLoading(questions);
    mainQuestionLoading(questions[0], 0);
  }
};

const mainQuestionLoading = (question, index) => {
  const mainQuestion = $("#main-question");
  mainQuestion.innerHTML = `
    <div>
        <div class="row">
            <div class="col c-12">
                <span class="question_order tc">
                    C${index + 1}
                </span>
            </div>
            <div class="col c-12">
                <p class="question_content">
                    ${question.noiDung}
                </p>
            </div>
        </div>
        <div class="question_answers" id="question-answer-${question.id}">
            <div class="row">
                <button onclick="chooseAnswer(event, 1, '${
                      question.a
                    }', ${question.id})" class="col c-12 answer" id="${question.id}-1"
                >
                    <span class="fl">A</span>
                    <p class="fl">${question.a}</p>
                </button>
                <button onclick="chooseAnswer(event, 2, '${
                      question.b
                    }', ${question.id})" class="col c-12 answer" id="${question.id}-2" >
                    <span class="fl">B</span>
                    <p class="fl">${question.b}</p>
                </button>
                <button onclick="chooseAnswer(event, 3, '${
                      question.c
                    }', ${question.id})" class="col c-12 answer" id="${question.id}-3" >
                    <span class="fl">C</span>
                    <p class="fl">${question.c}</p>
                </button>
                <button onclick="chooseAnswer(event, 4, '${
                      question.d
                    }', ${question.id})" class="col c-12 answer" id="${question.id}-4" >
                    <span class="fl">D</span>
                    <p class="fl">${question.d}</p>
                </button>
            </div>
        </div>
    </div>
  `;

  const rightAnswerItem = $(
    `.right-aws_item.right-${index + 1} .aws_item-box input:checked`
  );
  if (rightAnswerItem) {
    mainQuestion
      .querySelectorAll(".answer")[rightAnswerItem.name - 1].classList.add("selected");
  }

  if (!running) {
    mainQuestion.querySelectorAll(".answer").forEach((element) => element.disabled = "true")
    const answerSelect = $(`#right-answer-${question.id}`);
    if (answerSelect) {
      const answerSelectInput = answerSelect.querySelector("input:checked");
      const answers = mainQuestion.querySelectorAll(".answer");
      if (answerSelectInput) {
        if (question.dapAnDung == answerSelectInput.name)
          answers[answerSelectInput.name - 1].classList.add("checked--right");
        else
          answers[answerSelectInput.name - 1].classList.add("checked--wrong");
      }
    }
  }

  const listAnswerRight = $$(".right-aws_item");
  listAnswerRight.forEach((element, idx) => {
    element.classList.remove("selected");
    if (idx == index) element.classList.add("selected");
  })

  questionIndexCurrent = index;
};

const rightAnswerLoading = (listQuestion) => {
  const rigthAnswer = $("#right-answer");
  rigthAnswer.innerHTML = "";
  listQuestion.forEach((question, index) => {
    rigthAnswer.innerHTML += `
        <button class="right-aws_item fl right-${index + 1}" id="right-answer-${
      question.id
    }" onclick="showQuestion(event)">
            <span class="question-c">C${index + 1}</span>
            <div class="aws_item-box">
                <label>A</label>
                <input type="checkbox" name="1" id="${
                  question.a
                }" oninput="chooseAnswerRight(event, ${question.id})">
            </div>
            <div class="aws_item-box">
                <label>B</label>
                <input type="checkbox" name="2" id="${
                  question.b
                }" oninput="chooseAnswerRight(event, ${question.id})">
            </div>
            <div class="aws_item-box">
                <label>C</label>
                <input type="checkbox" name="3" id="${
                  question.c
                }" oninput="chooseAnswerRight(event, ${question.id})">
            </div>

            <div class="aws_item-box">
                <label>D</label>
                <input type="checkbox" name="4" id="${
                  question.d
                }" oninput="chooseAnswerRight(event, ${question.id})">
            </div>
        </button>
    `;
  });
};

const subQuestionLoading = (listQuestion) => {
  const subQuestion = $("#sub-question");
  subQuestion.innerHTML = "";
  listQuestion.forEach((question, index) => {
    subQuestion.innerHTML += `
      <div class="subquestion_item u-cf subquestion-${
        index + 1
      }" id="sub-question-${question.id}" onclick="showQuestion(event)">
        <span class="question_order tc fl">
            C${index + 1}
        </span>

        <div class="subAnswers fl">
            <div class="answer subAnswer fl" id="${question.a}-1">
                <span>A</span>
            </div>
            <div class="answer subAnswer fl" id="${question.b}-2">
                <span>B</span>
            </div>
            <div class="answer subAnswer fl" id="${question.c}-3">
                <span>C</span>
            </div>
            <div class="answer subAnswer fl" id="${question.d}-4">
                <span>D</span>
            </div>
        </div>
    </div>
    `;
  });
};

const showQuestion = async (e) => {
  let id;
  let number;
  if (e.target.classList.contains("right-aws_item")) {
    id = e.target.id.split("-")[2];
    number = e.target.classList[2].split("-")[1];
  }
  if (e.target.parentNode.classList.contains("right-aws_item")) {
    id = e.target.parentNode.id.split("-")[2];
    number = e.target.parentNode.classList[2].split("-")[1];
  }
  if (e.target.classList.contains("subquestion_item")) {
    id = e.target.id.split("-")[2];
    number = e.target.classList[2].split("-")[1];
  }
  if (e.target.parentNode.classList.contains("subquestion_item")) {
    id = e.target.parentNode.id.split("-")[2];
    number = e.target.parentNode.classList[2].split("-")[1];
  }
  if (e.target.parentNode.parentNode.classList.contains("subquestion_item")) {
    id = e.target.parentNode.parentNode.id.split("-")[2];
    number = e.target.parentNode.parentNode.classList[2].split("-")[1];
  }
  if ((id, number)) {
    const question = await fetch(`https://localhost:7002/api/cauhoi/${id}`, {
        method: "GET",
      })
      .then((res) => res.json())
      .then((data) => data)
      .catch((err) => console.log(err));

    mainQuestionLoading(question, parseInt(number) - 1);
    modalMobileToggle();
  }
};

const showQuestionPrev = () => {
  if (questionIndexCurrent >= 1) {
    const index = questionIndexCurrent - 1;
    mainQuestionLoading(questions[index], index);
  }
};

const showQuestionNext = () => {
  if (questionIndexCurrent < questions.length - 1) {
    const index = questionIndexCurrent + 1;
    mainQuestionLoading(questions[index], index);
  }
};

const chooseAnswer = (e, answerSelect, answer, id) => {
  const allAnswer = $$(`#question-answer-${id} .answer`);
  allAnswer.forEach((asw) => {
    asw.classList.remove("selected");
    if (asw.id.split("-")[1] == answerSelect)
      asw.classList.add("selected");
  });
  const rightAnswer = $(`#right-answer-${id}`);
  const rightAnswerItems = rightAnswer.querySelectorAll(".aws_item-box input");
  rightAnswerItems.forEach((item) => {
    item.checked = false;
    if (item.name == answerSelect) item.checked = true;
  });
  const subQuestionItems = $$(`#sub-question-${id} .subAnswer`);
  subQuestionItems.forEach((item) => {
    item.classList.remove("selected");
    const aswSelected = item.id.split("-");
    if (aswSelected[aswSelected.length - 1] == answerSelect)
      item.classList.add("selected");
  });
};

const chooseAnswerRight = (e, id) => {
  const rightAnswer = e.target.parentNode.parentNode;
  const rightAnswerItems = rightAnswer.querySelectorAll(".aws_item-box input");
  rightAnswerItems.forEach((item) => {
    if (item.name !== e.target.name) item.checked = false;
    else item.checked = true;
  });
  const questionAnswer = $$(`#question-answer-${id} .answer`);
  questionAnswer.forEach((asw) => {
    asw.classList.remove("selected");
    const aswSelected = asw.id.split("-");
    if (e.target.name == aswSelected[aswSelected.length - 1])
      asw.classList.add("selected");
  });
  const subquestionAnswer = $$(`#sub-question-${id} .answer`);
  subquestionAnswer.forEach((asw) => {
    asw.classList.remove("selected");
    const aswSelected = asw.id.split("-");
    if (e.target.name == aswSelected[aswSelected.length - 1])
      asw.classList.add("selected");
  });
};

const resultHandle = async () => {
  running = false;
  const allAnswer = $$(".right-aws_item.fl");
  let correct = 0;

  allAnswer.forEach(async (answer, index) => {
    const ansMobile = $(`.subquestion-${index + 1} .subAnswer.selected`)
    const ans = answer.querySelector(".aws_item-box input:checked");
    if (ans) {
      if (questions[index].dapAnDung == ans.name) {
        correct++;
        answer.classList.add("correct");
        ansMobile.classList.add("checked--right")
      } else {
        answer.classList.add("error");
        ansMobile.classList.add("checked--wrong")
      }
    } else {
      answer.classList.add("error");
    }
  });
  const score = (correct / questions.length) * 100;
  setTimeout(() => {
    showResult(score);
  }, 500);
};

const showResult = (score) => {
  clearInterval(x);

  const resultModal = $("#relt-modal");
  resultModal.classList.toggle("active");

  const btnCheck = $("#btn-check");
  btnCheck.disabled = true;
  const finishedMobile = $("#finished-mobile");
  finishedMobile.disabled = true;

  const questionAnswer = $$(".question_answers .answer");
  const answerRight = $$(".aws_item-box input");
  answerRight.forEach((ans) => {
    ans.disabled = true;
  });

  const scoreResult = resultModal.querySelector("#score-result");
  scoreResult.innerText = `${Math.round(score)} / 100`;

  let timeSecond = 60 - seconds;
  let timeMinute =
    timeSecond == 0 ? topic.thoiGian - minutes : topic.thoiGian - minutes - 1;
  if (timeSecond == 60) {
    timeMinute += 1;
    timeSecond = 0;
    console.log(timeMinute);
  }
  const timeResult = $("#time-result");
  timeResult.innerText = timeMinute + " phút " + timeSecond + " giây";
};

const closeModal = () => {
  const resultModal = $("#relt-modal");
  resultModal.classList.remove("active");
};

const closeResult = () => {
  closeModal();
  reworkFunction();
};

const showDetail = () => {
  closeResult();
  const questionAnswer = $(".right-aws_item");
  const index =
    parseInt(
      questionAnswer.classList[2].split("-")[
        questionAnswer.classList[2].split("-").length - 1
      ]
    ) - 1;
  const id =
    questionAnswer.id.split("-")[questionAnswer.id.split("-").length - 1];

  showAnswerDetail(index, id);
};

const showAnswerDetail = async (index, id) => {
  const question = await fetch(`https://localhost:7002/api/cauhoi/${id}`, {
      method: "GET",
    })
    .then((res) => res.json())
    .then((data) => data)
    .catch((err) => console.log(err));
  mainQuestionLoading(question, index);
  reworkFunction();
};

const reworkFunction = () => {
  const rework = $("#rework button");
  rework.style.display = "inline-block";
};

const redoExam = () => {
  const result = confirm("Bạn có muốn làm lại không?");
  if (result)
    setTimeout(() => {
      const btnCheck = $("#btn-check");
      btnCheck.disabled = false;
      const finishedMobile = $("#finished-mobile");
      finishedMobile.disabled = false;
      const rework = $("#rework button");
      rework.style.display = "none";
      closeModal();
      checkPermissions();
    }, 500);
};

const clearDataLocal = () => {
  sessionStorage.removeItem("exam");
  sessionStorage.removeItem("subject");
  sessionStorage.removeItem("code");
};

const modalMobileToggle = () => {
  const bars = $("#bars");
  bars.classList.toggle("change");
  const subQes = $("#subQes");
  subQes.classList.toggle("active");
};

const exitExam = () => {
  setTimeout(() => {
    window.location.href = "./";
    clearDataLocal();
  }, 500);
};