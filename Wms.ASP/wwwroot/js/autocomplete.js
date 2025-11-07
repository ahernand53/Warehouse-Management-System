// Wms.ASP/wwwroot/js/autocomplete.js

/**
 * Autocomplete functionality for WMS web forms
 * Provides search-as-you-type functionality with debounce and keyboard navigation
 */

(function() {
    'use strict';

    class Autocomplete {
        constructor(inputElement, options = {}) {
            this.input = inputElement;
            this.options = {
                minLength: options.minLength || 2,
                debounceMs: options.debounceMs || 400,
                maxResults: options.maxResults || 20,
                apiUrl: options.apiUrl || '',
                displayField: options.displayField || 'displayText',
                valueField: options.valueField || 'value',
                onSelect: options.onSelect || null,
                filterResults: options.filterResults || null,
                ...options
            };
            
            this.dropdown = null;
            this.results = [];
            this.selectedIndex = -1;
            this.debounceTimer = null;
            this.abortController = null;
            this.isOpen = false;

            this.init();
        }

        init() {
            // Create dropdown container
            this.createDropdown();
            
            // Setup event listeners
            this.input.addEventListener('input', (e) => this.handleInput(e));
            this.input.addEventListener('keydown', (e) => this.handleKeyDown(e));
            this.input.addEventListener('blur', () => this.handleBlur());
            this.input.addEventListener('focus', () => {
                // If minLength is 0, show all results on focus
                if (this.options.minLength === 0 && !this.input.value) {
                    this.search('');
                } else if (this.input.value.length >= this.options.minLength) {
                    this.search(this.input.value);
                }
            });
            
            // Also trigger on click to show dropdown immediately
            this.input.addEventListener('click', () => {
                if (this.options.minLength === 0 && !this.input.value && !this.isOpen) {
                    this.search('');
                }
            });

            // Close dropdown when clicking outside
            document.addEventListener('click', (e) => {
                if (!this.input.contains(e.target) && !this.dropdown.contains(e.target)) {
                    this.close();
                }
            });
        }

        createDropdown() {
            this.dropdown = document.createElement('div');
            this.dropdown.className = 'autocomplete-dropdown';
            this.dropdown.style.cssText = `
                position: absolute;
                z-index: 1050;
                background: white;
                border: 1px solid #ced4da;
                border-radius: 0.375rem;
                max-height: 250px;
                overflow-y: auto;
                box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
                display: none;
                width: 100%;
                top: 100%;
                left: 0;
                margin-top: 2px;
            `;
            
            // Insert after input's parent (input-group or form-control container)
            const inputGroup = this.input.closest('.input-group');
            const container = inputGroup || this.input.parentElement;
            
            // Ensure container has relative positioning
            if (window.getComputedStyle(container).position === 'static') {
                container.style.position = 'relative';
            }
            
            container.appendChild(this.dropdown);
            
            // Adjust width to match input if inside input-group
            if (inputGroup) {
                this.updateDropdownWidth();
                // Update width on window resize
                this.resizeHandler = () => this.updateDropdownWidth();
                window.addEventListener('resize', this.resizeHandler);
            }
        }

        updateDropdownWidth() {
            if (this.input && this.dropdown) {
                const inputRect = this.input.getBoundingClientRect();
                this.dropdown.style.width = inputRect.width + 'px';
            }
        }

        handleInput(e) {
            const value = e.target.value.trim();
            
            // Clear previous debounce
            if (this.debounceTimer) {
                clearTimeout(this.debounceTimer);
            }

            // Cancel previous request
            if (this.abortController) {
                this.abortController.abort();
            }

            if (value.length < this.options.minLength) {
                // If minLength is 0 and value is empty, show all results
                if (this.options.minLength === 0 && value.length === 0) {
                    this.debounceTimer = setTimeout(() => {
                        this.search('');
                    }, this.options.debounceMs);
                } else {
                    this.close();
                }
                return;
            }

            // Debounce search
            this.debounceTimer = setTimeout(() => {
                this.search(value);
            }, this.options.debounceMs);
        }

        async search(term) {
            // Allow empty term if minLength is 0
            if (this.options.minLength > 0 && (!term || term.length < this.options.minLength)) {
                this.close();
                return;
            }
            
            // Normalize term (empty string is allowed if minLength is 0)
            const searchTerm = term || '';

            try {
                // Cancel previous request
                if (this.abortController) {
                    this.abortController.abort();
                }
                this.abortController = new AbortController();

                const url = this.buildSearchUrl(searchTerm);
                const response = await fetch(url, {
                    signal: this.abortController.signal,
                    headers: {
                        'Accept': 'application/json'
                    }
                });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const data = await response.json();
                
                // Apply custom filter if provided
                let results = Array.isArray(data) ? data : [];
                if (this.options.filterResults) {
                    results = this.options.filterResults(results, searchTerm);
                }

                this.results = results.slice(0, this.options.maxResults);
                this.render();
            } catch (error) {
                if (error.name !== 'AbortError') {
                    console.error('Autocomplete search error:', error);
                }
                this.close();
            }
        }

        buildSearchUrl(term) {
            // Normalize search term (trim, empty string is allowed if minLength is 0)
            const normalizedTerm = term ? term.trim() : '';
            
            const url = new URL(this.options.apiUrl, window.location.origin);
            // Only set term parameter if it's not empty (to allow backend to return all results)
            if (normalizedTerm) {
                url.searchParams.set('term', normalizedTerm);
            }
            
            // Add additional params from options
            if (this.options.itemId) {
                url.searchParams.set('itemId', this.options.itemId);
            }
            if (this.options.type) {
                url.searchParams.set('type', this.options.type);
            }

            return url.toString();
        }

        render() {
            if (this.results.length === 0) {
                this.close();
                return;
            }

            this.dropdown.innerHTML = '';
            this.selectedIndex = -1;

            this.results.forEach((result, index) => {
                const item = document.createElement('div');
                item.className = 'autocomplete-item';
                item.style.cssText = `
                    padding: 0.625rem 1rem;
                    cursor: pointer;
                    border-bottom: 1px solid #e9ecef;
                    transition: background-color 0.15s ease;
                    font-size: 0.9375rem;
                `;
                
                // Remove last border from last item
                if (index === this.results.length - 1) {
                    item.style.borderBottom = 'none';
                }
                
                item.textContent = result[this.options.displayField] || result.displayText || result.value || result;
                
                item.addEventListener('mouseenter', () => {
                    this.selectedIndex = index;
                    this.highlightItem();
                });
                
                item.addEventListener('mouseleave', () => {
                    // Keep selection on keyboard navigation
                });
                
                item.addEventListener('click', () => {
                    this.selectItem(result);
                });

                this.dropdown.appendChild(item);
            });

            this.updateDropdownWidth();
            this.open();
        }

        handleKeyDown(e) {
            if (!this.isOpen || this.results.length === 0) {
                return;
            }

            switch (e.key) {
                case 'ArrowDown':
                    e.preventDefault();
                    this.selectedIndex = Math.min(this.selectedIndex + 1, this.results.length - 1);
                    this.highlightItem();
                    this.scrollToSelected();
                    break;
                
                case 'ArrowUp':
                    e.preventDefault();
                    this.selectedIndex = Math.max(this.selectedIndex - 1, -1);
                    this.highlightItem();
                    this.scrollToSelected();
                    break;
                
                case 'Enter':
                    e.preventDefault();
                    if (this.selectedIndex >= 0 && this.selectedIndex < this.results.length) {
                        this.selectItem(this.results[this.selectedIndex]);
                    }
                    break;
                
                case 'Escape':
                    e.preventDefault();
                    this.close();
                    break;
            }
        }

        highlightItem() {
            const items = this.dropdown.querySelectorAll('.autocomplete-item');
            items.forEach((item, index) => {
                if (index === this.selectedIndex) {
                    item.style.backgroundColor = '#0d6efd';
                    item.style.color = 'white';
                    item.style.fontWeight = '500';
                } else {
                    item.style.backgroundColor = 'white';
                    item.style.color = '#212529';
                    item.style.fontWeight = 'normal';
                }
            });
        }

        scrollToSelected() {
            if (this.selectedIndex < 0) return;
            
            const items = this.dropdown.querySelectorAll('.autocomplete-item');
            if (items[this.selectedIndex]) {
                items[this.selectedIndex].scrollIntoView({ block: 'nearest', behavior: 'smooth' });
            }
        }

        selectItem(result) {
            const value = result[this.options.valueField] || result.value || result;
            this.input.value = value;
            this.close();

            // Call custom onSelect callback
            if (this.options.onSelect) {
                this.options.onSelect(result, value);
            }

            // Trigger change event
            this.input.dispatchEvent(new Event('change', { bubbles: true }));
        }

        handleBlur() {
            // Delay closing to allow click events to fire
            setTimeout(() => {
                if (document.activeElement !== this.input && !this.dropdown.contains(document.activeElement)) {
                    this.close();
                }
            }, 200);
        }

        open() {
            this.updateDropdownWidth();
            this.dropdown.style.display = 'block';
            this.isOpen = true;
            
            // Position dropdown below input (never above to avoid covering input)
            this.dropdown.style.top = '100%';
            this.dropdown.style.bottom = 'auto';
            this.dropdown.style.marginTop = '2px';
            this.dropdown.style.marginBottom = '0';
            
            // Ensure dropdown doesn't go off screen - adjust max-height if needed
            const inputRect = this.input.getBoundingClientRect();
            const spaceBelow = window.innerHeight - inputRect.bottom;
            const estimatedHeight = Math.min(250, this.results.length * 40);
            
            if (spaceBelow < estimatedHeight + 20) {
                // Reduce max-height to fit on screen
                const maxHeight = Math.max(100, spaceBelow - 20);
                this.dropdown.style.maxHeight = maxHeight + 'px';
            } else {
                this.dropdown.style.maxHeight = '250px';
            }
            
            this.highlightItem();
        }

        close() {
            this.dropdown.style.display = 'none';
            this.isOpen = false;
            this.selectedIndex = -1;
        }

        destroy() {
            if (this.debounceTimer) {
                clearTimeout(this.debounceTimer);
            }
            if (this.abortController) {
                this.abortController.abort();
            }
            if (this.resizeHandler) {
                window.removeEventListener('resize', this.resizeHandler);
            }
            if (this.dropdown && this.dropdown.parentNode) {
                this.dropdown.parentNode.removeChild(this.dropdown);
            }
        }
    }

    // Export to window
    window.Autocomplete = Autocomplete;

    // Helper function to initialize autocomplete on an input
    window.initAutocomplete = function(inputSelector, options) {
        const input = typeof inputSelector === 'string' 
            ? document.querySelector(inputSelector) 
            : inputSelector;
        
        if (!input) {
            console.error('Autocomplete: Input element not found', inputSelector);
            return null;
        }

        return new Autocomplete(input, options);
    };

})();

