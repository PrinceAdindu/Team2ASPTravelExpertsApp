// Fetch agencies and agents data from the backend
document.addEventListener("DOMContentLoaded", () => {
    fetch("/api/agencies")
        .then((response) => response.json())
        .then((data) => {
            const accordion = document.getElementById("accordion");
            Object.values(data).forEach((agency, index) => {
                const agencyId = `agency-${index}`;
                const agentsHtml = agency.agents
                    .map(
                        (agent) => `
                  <div class="col-md-4 mb-5">
                      <div class="education card">
                       <div class="overlay"></div>
                          <div class="circle"></div>
                          <div class="card-body">
                              <h5 class="card-title">${agent.AgtFirstName} ${agent.AgtMiddleInitial || ""
                            } ${agent.AgtLastName}</h5>
                              <p class="card-text"><strong>Position:</strong> ${agent.AgtPosition
                            }</p>
                              <p class="card-text"><strong>Email:</strong> ${agent.AgtEmail
                            }</p>
                              <p class="card-text"><strong>Phone:</strong> ${agent.AgtBusPhone
                            }</p>
                          </div>
                      </div>
                  </div>
              `
                    )
                    .join("");

                accordion.innerHTML += `
                  <div class="accordion-item">
                      <h2 class="accordion-header my-2" id="heading-${agencyId}">
                          <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse-${agencyId}" aria-expanded="false" aria-controls="collapse-${agencyId}">
                              <strong>${index + 1}. ${agency.AgncyCity
                    }</strong> - ${agency.AgncyProv}
                          </button>
                      </h2>
                      <div id="collapse-${agencyId}" class="accordion-collapse collapse ${index == 0 && "show"
                    }" aria-labelledby="heading-${agencyId}" data-bs-parent="#accordion">
                          <div class="accordion-body">
                              <p><strong>Address:</strong> ${agency.AgncyAddress
                    }, ${agency.AgncyCity}, ${agency.AgncyProv}, ${agency.AgncyPostal
                    }, ${agency.AgncyCountry}</p>
                              <p><strong>Phone:</strong> ${agency.AgncyPhone
                    }</p>
                              <p><strong>Fax:</strong> ${agency.AgncyFax}</p>
                              <div class="row">
                                  ${agentsHtml}
                              </div>
                          </div>
                      </div>
                  </div>
              `;
            });
        })
        .catch((error) => console.error("Error fetching data:", error));
});