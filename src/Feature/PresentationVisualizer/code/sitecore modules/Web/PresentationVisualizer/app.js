const urlParams = new URLSearchParams(window.location.search);
const itemId = urlParams.get("id");

axios
  .get(`/sitecore/api/presentationvisualization/${itemId}`)
  .then(({ data, status }) => {
    if (status === 200 && data) {
      const dataToTransform = data;
      const transformedData = transformChartData(dataToTransform);
      const simple_chart_config = {
        chart: {
          container: "#chart-root",
          connectors: { type: "step" }, // straight
          rootOrientation: "WEST"
        },

        nodeStructure: transformedData,
      };
      new Treant(simple_chart_config);

      setTimeout(() => {
        $(".node-name").each(function () {
          const nodeName = $(this).text();
          const typeOfNode = $(this)
            .parent()
            .children(".node-type")
            .first()
            .text();
          let dataBsContent = "<div>";
          if ($(this).parent().children(".node-datasource").length > 0) {
            const datasource = JSON.parse(
              $(this).parent().children(".node-datasource").first().text()
            );
            const {
              id: datasourceId,
              name: datasourceName,
              path: datasourcePath,
            } = datasource;
            dataBsContent += `<div><span class='bold'>Datasource:</span> ${datasourceName} <code>${datasourcePath}</code> ${datasourceId}<div>`;
          }
          if (
            $(this).parent().children(".node-parameters").length > 0
          ) {
            const renderingParameters = JSON.parse(
              $(this)
                .parent()
                .children(".node-parameters")
                .first()
                .text()
            );
            dataBsContent += `<div><span class='bold'>Rendering parameters:</span> `;
              for (let parameter of renderingParameters) {
              const { name, value } = parameter;
              dataBsContent += `<p class='key-value-list'>${name}: ${value}</p>`;
            }
            dataBsContent += "</div>";
          }

          dataBsContent += "</div>";
          $(this).wrap(`<a 
                  type="button"
                  class='btn btn-secondary node-wrapper'
                  rel='popover'  
                  data-bs-toggle='popover'
                  data-html='true'
                  title='${typeOfNode}: ${nodeName}' 
                  data-bs-content="${dataBsContent}">
              </a>`);
        });
        $(".node-type").each(function () {
          const nodeWrapper = $(this)
            .parent()
            .children(".node-wrapper")
            .first();
          const nodeType = $(this).text();
          nodeWrapper.prepend(
            `<div class="node-icon">${getNodeIcon(nodeType)}</div>`
          );
        });

        var popoverTriggerList = [].slice.call(
          document.querySelectorAll('[data-bs-toggle="popover"]')
        );
        var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
          return new bootstrap.Popover(popoverTriggerEl, { html: true });
        });
      }, 1000);

      $(".node-name").css({ fontSize: 12, paddingRight: 6 });
    } else {
      alert("Something went wrong");
    }
  });
