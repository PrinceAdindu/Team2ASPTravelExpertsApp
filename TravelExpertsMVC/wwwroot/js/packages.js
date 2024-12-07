document.addEventListener("DOMContentLoaded", () => {
    fetch("/api/packages")
        .then((response) => response.json())
        .then((data) => {
            const packageContainer = document.getElementById('package-container');
            data.forEach(pkg => {
                const card = `
                <div class="col-md-4 mb-4">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">${pkg.PkgName}</h5>
                            <p class="card-text">${pkg.PkgDesc}</p>
                            <p class="card-text"><strong>Start Date:</strong> ${new Date(pkg.PkgStartDate).toLocaleDateString()}</p>
                            <p class="card-text"><strong>End Date:</strong> ${new Date(pkg.PkgEndDate).toLocaleDateString()}</p>
                            <p class="card-text"><strong>Price:</strong> $${parseFloat(pkg.PkgBasePrice).toFixed(2)}</p>
                            <p class="card-text"><strong>Agency Commission:</strong> $${parseFloat(pkg.PkgAgencyCommission).toFixed(2)}</p>
                            <button class="btn btn-primary order-btn" data-package-id="${pkg.PackageId}">Order</button>
                        </div>
                    </div>
                </div>`;

                packageContainer.innerHTML += card;
            });

            document.querySelectorAll('.order-btn').forEach(button => {
                button.addEventListener('click', function () {
                    const packageId = this.getAttribute('data-package-id');
                    localStorage.setItem('packageId', packageId);
                    window.location.href = `./order/${packageId}`;
                });
            });
        });
});
