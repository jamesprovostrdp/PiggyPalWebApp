// Theme colors
const cardColours = ['#FF0000', '#FF8700', '#FFD300', '#DEFF0A', '#A1FF0A', '#0AFF99',
    '#0AEFFF', '#147DF5', '#580AFF', '#BE0AFF'];
let colourIndex = 0;


document.getElementById('addExpenseCategory').addEventListener('click', function () {
    const firstCard = document.querySelector('#expenseCategories .card');
    if (!firstCard) return; 

    const newCard = firstCard.cloneNode(true);
    newCard.style.backgroundColor = cardColours[colourIndex];

    colourIndex = (colourIndex + 1) % cardColours.length;

    const inputs = newCard.querySelectorAll('input');
    inputs.forEach(input => input.value = '');

    document.getElementById('expenseCategories').appendChild(newCard);
});

// add income card from clone, sets color form dictionary 
document.getElementById('addIncomeCategory').addEventListener('click', function () {
    const firstCard = document.querySelector('#incomeCategories .card'); 
    if (!firstCard) return;

    const newCard = firstCard.cloneNode(true);
    newCard.style.backgroundColor = cardColours[colourIndex];
    newCard.classList.add('incomeCategories');

    colourIndex = (colourIndex + 1) % cardColours.length;

    const inputs = newCard.querySelectorAll('input');
    inputs.forEach(input => input.value = '');

    document.getElementById('incomeCategories').appendChild(newCard);
});

// toggles between income and expenses 
let expenseCards = document.getElementById("expenseCategories");
let incomeCards = document.getElementById("incomeCategories");
let toggleButton = document.getElementById("toggleIncomeExpenseCards");
let addExpenseButton = document.getElementById("addExpenseCategory");
let addIncomeButton = document.getElementById("addIncomeCategory");

toggleButton.addEventListener("click", function () {
    incomeCards.classList.toggle("hidden");
    expenseCards.classList.toggle("hidden");

    if (incomeCards.classList.contains("hidden")) {
        addIncomeButton.style.visibility = "hidden";
        addExpenseButton.style.visibility = "visible";
        toggleButton.textContent = "Show Income";
    } else {
        addIncomeButton.style.visibility = "visible";
        addExpenseButton.style.visibility = "hidden";
        toggleButton.textContent = "Show Expenses";
    }
});

document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll('.editCard').forEach(button => {
        button.addEventListener('click', function () {
            const card = this.closest('.card');
            const expandCard = card.querySelector('.editCardContent');
            const cardName = card.querySelector('.cardName');

            expandCard.classList.toggle('hidden');

            if (expandCard.classList.contains('hidden')) {
                cardName.contentEditable = 'false';
            } else {
                cardName.contentEditable = 'true';
                cardName.focus();
            }
        });
    });

    document.addEventListener("click", function (event) {
        if (event.target.classList.contains("saveCard")) {
            const card = event.target.closest('.card');
            const cardName = card.querySelector('.cardName').textContent.trim();
            const budgetInput = card.querySelector('.budgetInput');
            const budgetList = document.getElementById('budgetList');
            const budgetValue = budgetInput.value.trim();

     
            if (!budgetValue || isNaN(budgetValue) || budgetValue <= 0) {
                alert("Please enter a valid budget amount.");
                return;
            }

            let existingBudgetItem = Array.from(document.querySelectorAll('.budgetItem')).find(item =>
                item.querySelector(".categoryLabel").textContent.includes(cardName)
            );

            if (!existingBudgetItem) {
                let budgetItem = document.createElement("div");
                budgetItem.classList.add("budgetItem");
                budgetItem.innerHTML = `
                <label class="categoryLabel">${cardName} - $<span class="budgetAmount">${budgetValue}</span></label>
                <div class="progressBarContainer">
                    <div class="progressBar" style="width: 0%;"></div>
                </div>
                <label>Spent: $<span class="spentAmount">0</span> / $${budgetValue}</label>
            `;

              
                budgetList.appendChild(budgetItem);
            } else {

                existingBudgetItem.querySelector(".budgetAmount").textContent = budgetValue;
                existingBudgetItem.querySelector(".spentAmount").textContent = "0";
                existingBudgetItem.querySelector(".progressBar").style.width = "0%";
            }

     
            card.querySelector('.editCardContent').classList.add('hidden');

    
            card.dataset.budget = budgetValue;
            card.dataset.spent = 0;

            alert("Budget saved successfully!");
        }
    });

    document.addEventListener("input", function (event) {
        if (event.target.classList.contains("cardInput")) {
            const card = event.target.closest('.card');
            let expenseAmount = parseFloat(event.target.value) || 0;

            let budget = parseFloat(card.dataset.budget) || 1; 
            let spentAmount = parseFloat(card.dataset.spent) + expenseAmount;
            card.dataset.spent = spentAmount; 

            
            let categoryLabel = card.querySelector('.cardName').textContent.trim();
            let budgetItems = document.querySelectorAll('.budgetItem');

            budgetItems.forEach(item => {
                if (item.querySelector('.categoryLabel').textContent.includes(categoryLabel)) {
                    let spentDisplay = item.querySelector('.spentAmount');
                    let progressBar = item.querySelector('.progressBar');

                    spentDisplay.textContent = spentAmount;
                    let progress = Math.min((spentAmount / budget) * 100, 100);
                    progressBar.style.width = `${progress}%`;
                }
            });

            event.target.value = ""; 
        }
    });


        document.querySelectorAll('.deleteCard').forEach(button => {
            button.addEventListener('click', function () {
                const card = this.closest('.card');
                if (confirm('Are you sure you want to delete this category?')) {
                    card.remove();
                }
            });

        });


   
        document.querySelectorAll('.cardInput').forEach(input => {
            input.addEventListener('change', function () {
                const card = this.closest('.card');
                let spent = parseFloat(this.value) || 0;
                let budget = parseFloat(card.dataset.budget) || 1;

                let spentAmount = parseFloat(card.dataset.spent) + spent;
                card.dataset.spent = spentAmount; 

                
                let categoryLabel = card.querySelector('.cardName').textContent.trim();
                let budgetItems = document.querySelectorAll('.budgetItem');

                budgetItems.forEach(item => {
                    if (item.querySelector(".categoryLabel").textContent.includes(categoryLabel)) {
                        let spentDisplay = item.querySelector('.spentAmount');
                        let progressBar = item.querySelector('.progressBar');

                        spentDisplay.textContent = spentAmount;
                        let progress = Math.min((spentAmount / budget) * 100, 100);
                        progressBar.style.width = `${progress}%`;
                    }
                });
            
        });
    });
});